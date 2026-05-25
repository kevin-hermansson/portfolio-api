using Portfolio.Api.Core.Services;

namespace Portfolio.Api.Endpoints;

public static class ProjectEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/projects", (ProjectService projectService) =>
        {
            var projects = projectService.GetProjects();
            return Results.Ok(projects);
        });

        app.MapGet("/projects/{id:int}", (int id, ProjectService projectService) =>
        {
            var project = projectService.GetProject(id);
            return project is null ? Results.NotFound() : Results.Ok(project);
        });
    }
}
