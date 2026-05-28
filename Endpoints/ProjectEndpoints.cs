using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Core.DTOs;
using Portfolio.Api.Core.Services;

namespace Portfolio.Api.Endpoints;

public static class ProjectEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/projects", async (ProjectService projectService) =>
        {
            var projects = await projectService.GetProjectsAsync();
            return Results.Ok(projects);
        })
        .WithName("GetProjects")
        .WithSummary("Get all projects")
        .WithDescription("Returns every portfolio project, including repository URL and technologies used.")
        .Produces<List<ProjectDto>>(StatusCodes.Status200OK);

        app.MapGet("/projects/{id:int}", async ([FromRoute] int id, ProjectService projectService) =>
        {
            if (id < 1)
            {
                return Results.BadRequest("Project id must be greater than zero.");
            }

            var project = await projectService.GetProjectAsync(id);
            return project is null ? Results.NotFound() : Results.Ok(project);
        })
        .WithName("GetProjectById")
        .WithSummary("Get a project by ID")
        .WithDescription("Returns a single portfolio project when the provided positive project ID exists.")
        .Produces<ProjectDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapPost("/projects", async (
            [FromBody] ProjectRequestDto projectRequest,
            ProjectService projectService) =>
        {
            if (string.IsNullOrWhiteSpace(projectRequest.Title) || projectRequest.Title.Length < 2 || projectRequest.Title.Length > 100)
            {
                return Results.BadRequest("Title must be between 2 and 100 characters.");
            }

            if (string.IsNullOrWhiteSpace(projectRequest.Description) || projectRequest.Description.Length < 10 || projectRequest.Description.Length > 500)
            {
                return Results.BadRequest("Description must be between 10 and 500 characters.");
            }

            if (string.IsNullOrWhiteSpace(projectRequest.RepositoryUrl))
            {
                return Results.BadRequest("Repository URL is required.");
            }

            if (projectRequest.Technologies is null || projectRequest.Technologies.Count == 0)
            {
                return Results.BadRequest("Add at least one technology.");
            }

            var project = await projectService.CreateProjectAsync(projectRequest);
            return Results.Created($"/projects/{project.Id}", project);
        })
        .WithName("CreateProject")
        .WithSummary("Create a project")
        .WithDescription("Creates a new portfolio project from the provided project details.")
        .Produces<ProjectDto>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapPut("/projects/{id:int}", async (
            [FromRoute] int id,
            [FromBody] ProjectRequestDto projectRequest,
            ProjectService projectService) =>
        {
            if (id < 1)
            {
                return Results.BadRequest("Project id must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(projectRequest.Title) || projectRequest.Title.Length < 2 || projectRequest.Title.Length > 100)
            {
                return Results.BadRequest("Title must be between 2 and 100 characters.");
            }

            if (string.IsNullOrWhiteSpace(projectRequest.Description) || projectRequest.Description.Length < 10 || projectRequest.Description.Length > 500)
            {
                return Results.BadRequest("Description must be between 10 and 500 characters.");
            }

            if (string.IsNullOrWhiteSpace(projectRequest.RepositoryUrl))
            {
                return Results.BadRequest("Repository URL is required.");
            }

            if (projectRequest.Technologies is null || projectRequest.Technologies.Count == 0)
            {
                return Results.BadRequest("Add at least one technology.");
            }

            var project = await projectService.UpdateProjectAsync(id, projectRequest);
            return project is null ? Results.NotFound() : Results.Ok(project);
        })
        .WithName("UpdateProject")
        .WithSummary("Update a project")
        .WithDescription("Updates an existing portfolio project when the provided positive project ID exists.")
        .Produces<ProjectDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapDelete("/projects/{id:int}", async ([FromRoute] int id, ProjectService projectService) =>
        {
            if (id < 1)
            {
                return Results.BadRequest("Project id must be greater than zero.");
            }

            var deleted = await projectService.DeleteProjectAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteProject")
        .WithSummary("Delete a project")
        .WithDescription("Deletes an existing portfolio project when the provided positive project ID exists.")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
