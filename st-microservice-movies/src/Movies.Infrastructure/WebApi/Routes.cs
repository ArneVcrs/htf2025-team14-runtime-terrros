using System.Net.Mime;
using Movies.Infrastructure.WebApi.Controllers;
using Movies.Infrastructure.WebApi.Shared.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Movies.Infrastructure.WebApi;

public static class Routes
{
    public static OpenApiInfo OpenApiInfo { get; } = new OpenApiInfo
    {
        Version = "v1",
        Title = "Movies API",
        Description = "A simple API to manage movies and movie events.",
        Contact = new OpenApiContact
        {
            Name = "Lauren Cattoor & Arne Vercruysse",
            Email = "Blabla"
        }
    };

    public static WebApplication MapRoutes(this WebApplication app)
    {
        MapMovieRoutes(app);
        return app;
    }

    public static void MapMovieRoutes(WebApplication app)
    {
        var movieGroup = app.MapGroup("api/movies")
        .WithTags("Movies")
        .WithDescription("Endppoint related to movies.")
        .WithOpenApi();

        movieGroup.MapPost("/", RegisterMovieController.Invoke)
            .WithName("RegisterMovie")
            .WithDescription("Register a new movie.")
            .WithMetadata(new ConsumesAttribute(MediaTypeNames.Application.Json))
            .AddEndpointFilter<BodyValidatorFilter<RegisterMovieBody>>()
            .WithOpenApi();

        
        
    }


    
}



