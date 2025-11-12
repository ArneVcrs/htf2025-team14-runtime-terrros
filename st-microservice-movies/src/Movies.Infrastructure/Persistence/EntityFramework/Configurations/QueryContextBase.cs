using Movies.Application.Contracts.Data;
using Movies.Domain.Movie;
using Movies.Infrastructure.Persistence.EntityFramework.Configurations.Data;
using Movies.Infrastructure.Persistence.EntityFramework.Configurations.Domain;
using Microsoft.EntityFrameworkCore;

namespace Movies.Infrastructure.Persistence.EntityFramework.Configurations;

public abstract class QueryContextBase : DbContext
{
    public DbSet<MovieData> Movies { get; set; }


   
   
    protected QueryContextBase()
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieDataConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
