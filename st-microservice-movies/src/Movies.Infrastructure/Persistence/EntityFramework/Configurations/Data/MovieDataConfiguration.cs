using Movies.Application.Contracts.Data;
using Movies.Domain.Movie;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movies.Infrastructure.Persistence.EntityFramework.Configurations.Data;

public sealed class MovieDataConfiguration : IEntityTypeConfiguration<MovieData>
{
    public void Configure(EntityTypeBuilder<MovieData> builder)
    {
        builder.ToTable("Movies");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id);
        builder.Property(m => m.Title);
        builder.Property(m => m.Description);
        builder.Property(m => m.Year);
        builder.Property(m => m.Duration);
        builder.Property(m => m.Genre);
        builder.Property(m => m.Actors);
        builder.Property(m => m.AgeRating);
        builder.Property(m => m.PosterUrl);
               
    }
}