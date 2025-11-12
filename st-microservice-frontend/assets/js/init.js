import { onDOMReady } from './filmRenderer.js';

function init() {
    console.log("Frontend initialized");
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', onDOMReady);
    } else {
        onDOMReady();
    }
}

init(); 