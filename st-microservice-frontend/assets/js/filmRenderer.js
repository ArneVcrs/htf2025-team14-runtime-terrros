// filmRenderer.js
// Fetches mock movies JSON and renders fancy movie cards into #movies-grid

function onDOMReady(){
	const grid = document.getElementById('movies-grid');
	if (!grid) return;

	const localPaths = [
		'./assets/mock/movies.json',
		'/st-microservice-movies/mock-responses/movies.json'
	];

	// Try to fetch the first path that responds
	(async function loadAndRender(){
		let data = null;
		for (const p of localPaths) {
			try {
				const res = await fetch(p, {cache: 'no-store'});
				if (!res.ok) throw new Error(`HTTP ${res.status}`);
				data = await res.json();
				break;
			} catch (err) {
				// try next
				// console.warn('fetch failed for', p, err);
			}
		}

		if (!data || !Array.isArray(data.movies)) {
			grid.innerHTML = '<div class="empty">Geen mock data gevonden. Plaats een JSON bij <code>assets/mock/movies.json</code>.</div>';
			return;
		}

		renderMovies(grid, data.movies);

		// wire Add Movie button (opens modal)
		const addBtn = document.getElementById('add-movie-btn');
		if (addBtn) addBtn.addEventListener('click', () => openAddMovieModal(grid));
	})();
};

function renderMovies(container, movies){
	container.innerHTML = '';
	movies.forEach(m => container.appendChild(createCard(m)));
}

function createCard(m){
	const card = document.createElement('article');
	card.className = 'movie-card';

	// Poster als <img> element
	const poster = document.createElement('img');
	poster.className = 'poster';
	poster.src = (typeof m.posterUrl === 'string' && m.posterUrl.length) ? m.posterUrl : '';
	poster.alt = m.title || 'Movie poster';
	poster.onerror = () => {
		poster.src = 'https://via.placeholder.com/200x300?text=No+Image'; // fallback
	};

	const body = document.createElement('div');
	body.className = 'card-body';

	const title = document.createElement('div');
	title.className = 'title';
	title.textContent = m.title || 'Untitled';

	const meta = document.createElement('div');
	meta.className = 'meta';
	const left = document.createElement('div');
	left.textContent = `${m.genre || ''} • ${m.year || ''}`;
	const chip = document.createElement('div');
	chip.className = 'chip';
	chip.textContent = `${m.duration || '?'}m`;
	meta.appendChild(left);
	meta.appendChild(chip);

	const desc = document.createElement('div');
	desc.className = 'desc';
	desc.textContent = m.description || '';

	const actorsEl = document.createElement('div');
	actorsEl.className = 'actors';
	const actorList = normalizeActors(m.actors);
	actorsEl.textContent = actorList.length ? `Starring: ${actorList.slice(0,3).join(', ')}${actorList.length>3 ? ' +'+(actorList.length-3):''}` : '';

	const footer = document.createElement('div');
	footer.className = 'footer';
	const age = document.createElement('div');
	age.className = 'age';
	age.textContent = m.ageRating || '';
	const details = document.createElement('div');
	details.className = 'meta';
	details.textContent = '';
	footer.appendChild(age);
	footer.appendChild(details);

	body.appendChild(meta);
	body.appendChild(title);
	body.appendChild(desc);
	body.appendChild(actorsEl);

	card.appendChild(poster);
	card.appendChild(body);
	card.appendChild(footer);

	return card;
}


function normalizeActors(actors){
	if (!actors) return [];
	if (Array.isArray(actors)) return actors.map(a => String(a).trim()).filter(Boolean);
	if (typeof actors === 'string') {
		return actors.split(',').map(s => s.trim()).filter(Boolean);
	}
	return [];
}



export { onDOMReady };

// --- Add movie modal/form ---
function openAddMovieModal(grid){
	const root = document.getElementById('modal-root');
	if (!root) return;

	// build modal
	const backdrop = document.createElement('div');
	backdrop.className = 'modal-backdrop';

	const content = document.createElement('div');
	content.className = 'modal-content';

	const header = document.createElement('div');
	header.className = 'modal-header';
	const h = document.createElement('h3'); h.textContent = 'Add a new movie';
	const closeBtn = document.createElement('button'); closeBtn.className = 'modal-close'; closeBtn.innerHTML = '✕';
	header.appendChild(h); header.appendChild(closeBtn);

	const form = document.createElement('form'); form.className = 'movie-form';

	// fields
	form.innerHTML = `
		<label>Title<input name="title" required></label>
		<label>Genre<input name="genre"></label>
		<label>Description<textarea name="description" rows="3"></textarea></label>
		<div class="row">
		  <label>Year<input name="year" type="number" min="1800" max="2100"></label>
		  <label>Duration (min)<input name="duration" type="number" min="1"></label>
		</div>
		<label>Actors (comma separated)<input name="actors"></label>
		<div class="row">
		  <label>Age Rating<input name="ageRating"></label>
		  <label>Poster URL<input name="posterUrl" type="url"></label>
		</div>
		<div class="form-actions">
		  <button type="button" class="btn btn-ghost" data-action="cancel">Cancel</button>
		  <button type="submit" class="btn btn-primary">Add Movie</button>
		</div>
	`;

	content.appendChild(header);
	content.appendChild(form);
	backdrop.appendChild(content);
	root.appendChild(backdrop);

	function close(){ root.removeChild(backdrop); }

	closeBtn.addEventListener('click', close);
	backdrop.addEventListener('click', (e) => { if (e.target === backdrop) close(); });
	form.addEventListener('submit', async (e) => {
		e.preventDefault();
		const fd = new FormData(form);
		const movie = {
			id: (typeof crypto !== 'undefined' && crypto.randomUUID) ? crypto.randomUUID() : ('id-'+Date.now()),
			title: (fd.get('title')||'').toString().trim(),
			genre: (fd.get('genre')||'').toString().trim(),
			description: (fd.get('description')||'').toString().trim(),
			year: Number(fd.get('year')) || undefined,
			duration: Number(fd.get('duration')) || undefined,
			actors: (fd.get('actors')||'').toString().trim(),
			ageRating: (fd.get('ageRating')||'').toString().trim(),
			posterUrl: (fd.get('posterUrl')||'').toString().trim()
		};

		// Basic validation
		if (!movie.title) { alert('Title is required'); return; }

		// Try to POST to API endpoint; change endpoint as needed later.
		const endpoint = '/api/movies';
		try {
			const resp = await fetch(endpoint, {method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify(movie)});
			if (resp.ok) {
				// success - try to use returned object if provided
				let saved = movie;
				try { saved = await resp.json(); } catch(_){}
				addMovieToGrid(grid, saved);
				close();
				return;
			}
		} catch (err) {
			// network error - fallback to UI add
		}

		// fallback: directly add to UI
		addMovieToGrid(grid, movie);
		close();
	});

	// cancel
	form.querySelector('[data-action="cancel"]').addEventListener('click', (e) => { e.preventDefault(); close(); });
}

function addMovieToGrid(grid, movie){
	// ensure actors are string or array; keep as-is
	const card = createCard(movie);
	grid.insertBefore(card, grid.firstChild);
}