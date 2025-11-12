using Domaincrafters.Application;
using Movies.Application.Contracts.Ports;
using Movies.Domain.Movie;
using Movies.Infrastructure.Persistence.EntityFramework.Configurations;
using Movies.Infrastructure.Persistence.EntityFramework.Queries;
using Movies.Infrastructure.Persistence.EntityFramework.Repositories;
using Movies.Infrastructure.Persistence.EntityFramework.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Movies.Main.Modules.Persistence;

public static class EntityFrameworkServices
{
    public static IServiceCollection AddEntityFrameworkServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddDbContext(configuration)
            .AddRepositories()
            .AddQueries()
            .AddScoped<IUnitOfWork, EntityFrameworkUoW>();

        return services;
    }

    private static IServiceCollection AddDbContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        string databaseProvider = configuration.GetValue<string>("Database:Provider")!;
        switch (databaseProvider)
        {
            case "PostgreSQL":
                services.AddDbContext<DomainContextBase, DomainContextPostgres>();
                services.AddDbContext<QueryContextBase, QueryContextPostgres>();
                break;
            default:
                throw new NotSupportedException($"Database provider '{databaseProvider}' is not supported.");
        }

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services
    )
    {
        return services
            .AddScoped<IUnitOfWork, EntityFrameworkUoW>()
            .AddScoped<IMovieRepository, MovieRepository>();
    }

    private static IServiceCollection AddQueries(
        this IServiceCollection services
    )
    {
        return services
            .AddScoped<IAllMoviesQuery, AllMoviesQuery>();
            
    }
    
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DomainContextBase>();

        context.Database.Migrate();
        Console.WriteLine("Database migrated.");

        return app;
    }
}