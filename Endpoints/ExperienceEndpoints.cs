using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Core.DTOs;
using Portfolio.Api.Core.Services;
using Portfolio.Api.Core.Validation;

namespace Portfolio.Api.Endpoints;

public static class ExperienceEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/experiences", async (ExperienceService experienceService) =>
        {
            var experiences = await experienceService.GetExperiencesAsync();
            return Results.Ok(experiences);
        })
        .WithName("GetExperiences")
        .WithSummary("Get all experiences")
        .WithDescription("Returns every portfolio experience, including dates, description and technologies used.")
        .Produces<List<ExperienceDto>>(StatusCodes.Status200OK);

        app.MapGet("/experiences/{id:int}", async ([FromRoute] int id, ExperienceService experienceService) =>
        {
            var validationError = RequestValidator.ValidatePositiveId(id, "Experience");
            if (validationError is not null)
            {
                return Results.BadRequest(validationError);
            }

            var experience = await experienceService.GetExperienceAsync(id);
            return experience is null ? Results.NotFound() : Results.Ok(experience);
        })
        .WithName("GetExperienceById")
        .WithSummary("Get an experience by ID")
        .WithDescription("Returns a single portfolio experience when the provided positive experience ID exists.")
        .Produces<ExperienceDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapPost("/experiences", async (
            [FromBody] ExperienceRequestDto experienceRequest,
            ExperienceService experienceService) =>
        {
            var validationError = RequestValidator.ValidateExperience(experienceRequest);
            if (validationError is not null)
            {
                return Results.BadRequest(validationError);
            }

            var experience = await experienceService.CreateExperienceAsync(experienceRequest);
            return Results.Created($"/experiences/{experience.Id}", experience);
        })
        .WithName("CreateExperience")
        .WithSummary("Create an experience")
        .WithDescription("Creates a new portfolio experience from the provided experience details.")
        .Produces<ExperienceDto>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapPut("/experiences/{id:int}", async (
            [FromRoute] int id,
            [FromBody] ExperienceRequestDto experienceRequest,
            ExperienceService experienceService) =>
        {
            var idValidationError = RequestValidator.ValidatePositiveId(id, "Experience");
            if (idValidationError is not null)
            {
                return Results.BadRequest(idValidationError);
            }

            var validationError = RequestValidator.ValidateExperience(experienceRequest);
            if (validationError is not null)
            {
                return Results.BadRequest(validationError);
            }

            var experience = await experienceService.UpdateExperienceAsync(id, experienceRequest);
            return experience is null ? Results.NotFound() : Results.Ok(experience);
        })
        .WithName("UpdateExperience")
        .WithSummary("Update an experience")
        .WithDescription("Updates an existing portfolio experience when the provided positive experience ID exists.")
        .Produces<ExperienceDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapDelete("/experiences/{id:int}", async ([FromRoute] int id, ExperienceService experienceService) =>
        {
            var validationError = RequestValidator.ValidatePositiveId(id, "Experience");
            if (validationError is not null)
            {
                return Results.BadRequest(validationError);
            }

            var deleted = await experienceService.DeleteExperienceAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteExperience")
        .WithSummary("Delete an experience")
        .WithDescription("Deletes an existing portfolio experience when the provided positive experience ID exists.")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
