
using Movies.Domain.Movie;
using Movies.Infrastructure.Persistence.EntityFramework.Configurations.Domain;
using Microsoft.EntityFrameworkCore;

namespace Movies.Infrastructure.Persistence.EntityFramework.Configurations;

public abstract class DomainContextBase : DbContext
{
    public DbSet<Movie> Movies { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
      

        base.OnModelCreating(modelBuilder);


        modelBuilder.Seed();
    }
}