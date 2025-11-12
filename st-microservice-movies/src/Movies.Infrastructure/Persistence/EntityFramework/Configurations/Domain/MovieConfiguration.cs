using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Movie;

namespace Movies.Infrastructure.Persistence.EntityFramework.Configurations.Domain;

public sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");
    
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
            .ValueGeneratedNever();
        builder.Property(m => m.Id)
            .HasConversion(id => id.Value, id => new MovieId(id));

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(m => m.Year)
            .IsRequired()
            .HasMaxLength(4);    
        
        builder.Property(m => m.Duration)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(m => m.Genre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Actors)
            .IsRequired()
            .HasMaxLength(1000);  

        builder.Property(m => m.AgeRating)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(m => m.PosterUrl)
            .IsRequired()
            .HasMaxLength(1000);          
    }
}