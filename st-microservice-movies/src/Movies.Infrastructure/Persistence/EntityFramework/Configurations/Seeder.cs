/*
using Howestprime.Movies.Domain.Facilities;
using Howestprime.Movies.Domain.Movies;
*/
using Movies.Domain.Movie;
using Microsoft.EntityFrameworkCore;

namespace Movies.Infrastructure.Persistence.EntityFramework.Configurations;
/**
    * Seeder class to seed the database with initial data.
    * Replace the commented code with actual data seeding logic.
*/

public static class Seeder
{
    
    private static readonly IList<Movie> _movies = [
        Movie.Create("The Matrix",
        "A computer hacker learns from mysterious rebels about the true nature of his reality",
        2025,
        136,
        "Sci-fi",
        "Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss",
        16,
        "https://www.imdb.com/title/tt0133093/", new MovieId("ebfb9308-6c61-4608-af77-394448808e9b")),

        Movie.Create(
        "The Matrix Reloaded",
        "The human city of Zion defends itself against the massive invasion of the machines as Neo fights to end the war at another front while also opposing the rogue Agent Smith.",
        2025,
        138,
        "Sci-fi",
        "Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss",
        16,
        "https://www.imdb.com/title/tt0234215/", new MovieId("fb258d1a-10a2-4bf9-85cd-ca83585d1ee5")),
    ];

    

    public static void Seed(this ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Movie>().HasData(_movies);
        
    }
}