using System.Security.Cryptography.X509Certificates;
using Domaincrafters.Domain;
using Movies.Domain.Movie;

namespace Movies.Application.Contracts.Data;

public sealed record MovieData{
    public required string Id { get; init; }
    public required string PosterUrl { get; init; }
    public required string Title { get; init; }
    public required string Genre { get; init; }
    public required int AgeRating { get; init; }
    public required int Year { get; init; }
    public required int Duration { get; init; }
    public required string Actors { get; init; }
    public required string Description { get; init; }
    //public required List<MovieEventData> Events { get; init; } = new List<MovieEventData>();
}
    