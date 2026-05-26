using Portfolio.Api.Endpoints;
using Portfolio.Api.Core.Services;
using Portfolio.Api.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProjectRepository>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "CV Portfolio API",
        Version = "v1",
        Description = "API endpoints for portfolio project data."
    }); 
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/projects"))
    .ExcludeFromDescription();
ProjectEndpoints.Map(app);

app.Run();
