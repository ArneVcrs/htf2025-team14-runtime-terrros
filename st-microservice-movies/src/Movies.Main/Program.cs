using Movies.Main.Modules;
using Movies.Main.Modules.Persistence;
using Movies.Main.Modules.WebApi;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
/**
TODO: Only add the remaining modules if they are properly configured in the appsettings.json, and other settings files.
Otherwise, the app will not start.
**/
builder
    .Services
        .AddPersistenceModule(configuration)
        .AddWebApiModule(configuration);
    

var app = builder
    .Build();

app.UseCors(); // Ensure CORS is applied before other middleware

app.UsePersistenceModule()
   .UseWebApiModule();

await app.RunAsync();
