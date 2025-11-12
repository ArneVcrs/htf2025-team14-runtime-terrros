using Microsoft.Extensions.Logging;
using Movies.Domain.Movie;
using Domaincrafters.Application;

namespace Movies.Application.Movies;

public sealed record RegisterMovieInput(
    string Title,
    string Description,
    int Duration,
    string Genre,
    int Year,
    string Actors,
    int AgeRating,
    string PosterUrl
);

public sealed class RegisterMovie (
    IMovieRepository MovieRepository,
    IUnitOfWork UnitOfWork,
    ILogger<RegisterMovie> Logger
): IUseCase<RegisterMovieInput, Task<string>>
{
    private readonly ILogger<RegisterMovie> _logger = Logger;
    private readonly IMovieRepository _movieRepository = MovieRepository;
    private readonly IUnitOfWork _unitOfWork = UnitOfWork;

    public async Task<string> Execute(RegisterMovieInput input)
    {
        MovieId movieId = new();

        Movie movie = Movie.Create(
            input.Title,
            input.Description,
            input.Year,
            input.Duration,
            input.Genre,
            input.Actors,
            input.AgeRating,
            input.PosterUrl,
            movieId
        );

        await _movieRepository.Save(movie);

        await _unitOfWork.Do();

        _logger.LogInformation("Movie {MovieId} registered", movieId.Value);

        return movieId.Value;
    }

    
}

