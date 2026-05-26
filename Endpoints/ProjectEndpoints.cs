using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Core.DTOs;
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
        })
        .WithName("GetProjects")
        .WithSummary("Get all projects")
        .WithDescription("Returns every portfolio project, including repository URL and technologies used.")
        .Produces<List<ProjectDto>>(StatusCodes.Status200OK);

        app.MapGet("/projects/{id:int}", Results<Ok<ProjectDto>, NotFound, ValidationProblem> (
            [FromRoute]
            [Range(1, int.MaxValue)]
            int id,
            ProjectService projectService) =>
        {
            if (id < 1)
            {
                return TypedResults.ValidationProblem(new Dictionary<string, string[]>
                {
                    [nameof(id)] = ["Project id must be greater than zero."]
                });
            }

            var project = projectService.GetProject(id);
            return project is null ? TypedResults.NotFound() : TypedResults.Ok(project);
        })
        .WithName("GetProjectById")
        .WithSummary("Get a project by ID")
        .WithDescription("Returns a single portfolio project when the provided positive project ID exists.")
        .Produces<ProjectDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem();
    }
}
