using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Endpoints;
using Portfolio.Api.Core.Services;
using Portfolio.Api.Data;
using Portfolio.Api.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PortfolioDatabase")
    ?? "Data Source=portfolio.db";

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddScoped<ProjectRepository>();
builder.Services.AddScoped<SkillRepository>();
builder.Services.AddScoped<ExperienceRepository>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<SkillService>();
builder.Services.AddScoped<ExperienceService>();
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


app.MapGet("/", () => Results.Redirect("/swagger"))
    .ExcludeFromDescription();
ProjectEndpoints.Map(app);
SkillEndpoints.Map(app);
ExperienceEndpoints.Map(app);

app.Run();
