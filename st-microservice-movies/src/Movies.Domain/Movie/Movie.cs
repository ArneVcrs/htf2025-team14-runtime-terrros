
using Domaincrafters.Domain;
namespace Movies.Domain.Movie;

public sealed class MovieId(string? id = "") : UuidEntityId(id);

public class Movie: Entity<MovieId>
{
     public string Title { get; private set; }
    public string Description { get; private set; }
    public int Year { get; private set; }
    public int Duration { get; private set; }
    public string Genre { get; private set; }
    public string Actors { get; private set; }
    public int AgeRating { get; private set; }
    public string PosterUrl { get; private set; }

    public static Movie Create(string title, string description, int year, int duration, string genre, string actors, int ageRating, string posterUrl, MovieId? id = null)
    {
        MovieId movieId = id ?? new MovieId();

        Movie movie = new(movieId, title, description, year, duration, genre, actors, ageRating, posterUrl);
        movie.ValidateState();

        return movie;
    }

    private Movie(MovieId id, string title, string description, int year, int duration, string genre, string actors, int ageRating, string posterUrl) : base(id)
    {
        Title = title;
        Description = description;
        Year = year;
        Duration = duration;
        Genre = genre;
        Actors = actors;
        AgeRating = ageRating;
        PosterUrl = posterUrl;
    }

    public override void ValidateState()
    {
        EnsureTitleIsNotEmpty(Title);
        EnsureDescriptionIsNotEmpty(Description);
        EnsureYearIsValid(Year);
        EnsureDurationIsValid(Duration);
        EnsureGenreIsNotEmpty(Genre);
        EnsureActorsIsNotEmpty(Actors);
    }

    private static void EnsureTitleIsNotEmpty(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty.");
        }
    }
    private static void EnsureDescriptionIsNotEmpty(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description cannot be empty.");
        }
    }

    private static void EnsureYearIsValid(int year)
    {
        if (year < DateTime.Now.Year)
        {
            throw new ArgumentException("Year must be greater than or equal to the current year.");
        }
    }

    private static void EnsureDurationIsValid(int duration)
    {
        if (duration <= 0)
        {
            throw new ArgumentException("Duration must be a positive number.");
        }
    }

    private static void EnsureGenreIsNotEmpty(string genre)
    {
        if (string.IsNullOrWhiteSpace(genre))
        {
            throw new ArgumentException("Genre cannot be empty.");
        }
    }

    private static void EnsureActorsIsNotEmpty(string actors)
    {
        if (string.IsNullOrWhiteSpace(actors))
        {
            throw new ArgumentException("Actors cannot be empty.");
        }
    }

    public void UpdateMovieDetails(
        string title,
        string description,
        int year,
        int duration,
        string genre,
        string actors,
        int ageRating,
        string posterUrl)
    {
        Title = title;
        Description = description;
        Year = year;
        Duration = duration;
        Genre = genre;
        Actors = actors;
        AgeRating = ageRating;
        PosterUrl = posterUrl;

    }
}