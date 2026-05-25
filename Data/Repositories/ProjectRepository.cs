using Portfolio.Api.Data.Models;

namespace Portfolio.Api.Data.Repositories;

public class ProjectRepository
{
    private readonly List<Project> _projects = new()
    {
        new Project
        {
            Id = 1,
            Title = "CV Portfolio API",
            Description = "A Minimal API backend for serving portfolio data.",
            RepositoryUrl = "https://github.com/example/cv-portfolio",
            Technologies = new List<string> { "ASP.NET Core", "Minimal API", "C#" }
        }
    };

    public List<Project> GetAll()
    {
        return _projects;
    }

    public Project? GetById(int id)
    {
        foreach (var project in _projects)
        {
            if (project.Id == id)
            {
                return project;
            }
        }

        return null;
    }
}
