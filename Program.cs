using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Endpoints;
using Portfolio.Api.Core.Services;
using Portfolio.Api.Data;
using Portfolio.Api.Data.Repositories;
using Portfolio.Api.Data.Seed;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PortfolioDatabase")
    ?? "Data Source=portfolio.db";

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddScoped<ProjectRepository>();
builder.Services.AddScoped<SkillRepository>();
builder.Services.AddScoped<ExperienceRepository>();
builder.Services.AddScoped<ProfileRepository>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<SkillService>();
builder.Services.AddScoped<ExperienceService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "CV Portfolio API",
        Version = "v1",
        Description = "API endpoints for portfolio data."
    }); 
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5174", "http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

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
ProfileEndpoints.Map(app);

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
    await dbContext.Database.MigrateAsync();
    await PortfolioDbSeeder.SeedAsync(dbContext);
}

app.Run();
