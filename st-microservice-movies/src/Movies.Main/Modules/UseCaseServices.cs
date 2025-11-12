using Domaincrafters.Application;
using Movies.Application.Movies;
using Movies.Application.Contracts.Data;
using Movies.Application.Contracts.Ports;
using Movies.Domain.Movie;


namespace Movies.Main.Modules;

public static class UseCaseServices
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        return services
            .AddRegisterMovie();
    }

    private static IServiceCollection AddRegisterMovie(this IServiceCollection services)
    {
        return services
            .AddScoped<IUseCase<RegisterMovieInput, Task<string>>>(serviceProvider =>
            {
                var movieRepository = serviceProvider.GetRequiredService<IMovieRepository>();
                var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                var logger = serviceProvider.GetRequiredService<ILogger<RegisterMovie>>();

                return new RegisterMovie(movieRepository, unitOfWork, logger);

            });

    }

    
    
}