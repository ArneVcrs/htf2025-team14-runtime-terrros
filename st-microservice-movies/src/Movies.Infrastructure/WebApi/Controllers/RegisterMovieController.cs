using System.ComponentModel.DataAnnotations;
using Domaincrafters.Application;
using Movies.Application.Movies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Movies.Infrastructure.WebApi.Controllers;

public sealed record RegisterMovieBody
{
    [Required]
    public required string Title { get; init; }
    [Required]
    public required string Description { get; init; }
    [Required]
    public required int Duration { get; init; }
   [Required]
    public required string Genre { get; init; }
    [Required]
    public required int Year { get; init; }
    [Required]
    public required string Actors { get; init; }
    [Required]
    public required int AgeRating { get; init; }
    [Required]
    public required string PosterUrl { get; init; }
}

public sealed class RegisterMovieController
{
    public static async Task<Results<Created, BadRequest>> Invoke(
        [FromBody] RegisterMovieBody body,
        [FromServices] IUseCase<RegisterMovieInput, Task<string>> registerMovie
    )
    {
        RegisterMovieInput registerMovieInput = new(
            body.Title,
            body.Description,
            body.Duration,
            body.Genre,
            body.Year,
            body.Actors,
            body.AgeRating,
            body.PosterUrl
        );

        string movieId = await registerMovie.Execute(registerMovieInput);

        return TypedResults.Created($"/movies/{movieId}");
    }
}