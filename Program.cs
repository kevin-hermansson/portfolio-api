using Portfolio.Api.Endpoints;
using Portfolio.Api.Core.Services;
using Portfolio.Api.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProjectRepository>();
builder.Services.AddScoped<ProjectService>();

var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/projects"));
ProjectEndpoints.Map(app);

app.Run();
