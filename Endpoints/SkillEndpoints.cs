using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Core.DTOs;
using Portfolio.Api.Core.Services;
using Portfolio.Api.Core.Validation;

namespace Portfolio.Api.Endpoints;

public static class SkillEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/skills", async (SkillService skillService) =>
        {
            var skills = await skillService.GetSkillsAsync();
            return Results.Ok(skills);
        })
        .WithName("GetSkills")
        .WithSummary("Get all skills")
        .WithDescription("Returns every portfolio skill, including category and skill level.")
        .Produces<List<SkillDto>>(StatusCodes.Status200OK);

        app.MapGet("/skills/{id:int}", async ([FromRoute] int id, SkillService skillService) =>
        {
            var validationError = RequestValidator.ValidatePositiveId(id, "Skill");
            if (validationError is not null)
            {
                return Results.BadRequest(validationError);
            }

            var skill = await skillService.GetSkillAsync(id);
            return skill is null ? Results.NotFound() : Results.Ok(skill);
        })
        .WithName("GetSkillById")
        .WithSummary("Get a skill by ID")
        .WithDescription("Returns a single portfolio skill when the provided positive skill ID exists.")
        .Produces<SkillDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapPost("/skills", async (
            [FromBody] SkillRequestDto skillRequest,
            SkillService skillService) =>
        {
            var validationError = RequestValidator.ValidateSkill(skillRequest);
            if (validationError is not null)
            {
                return Results.BadRequest(validationError);
            }

            var skill = await skillService.CreateSkillAsync(skillRequest);
            return Results.Created($"/skills/{skill.Id}", skill);
        })
        .WithName("CreateSkill")
        .WithSummary("Create a skill")
        .WithDescription("Creates a new portfolio skill from the provided skill details.")
        .Produces<SkillDto>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapPut("/skills/{id:int}", async (
            [FromRoute] int id,
            [FromBody] SkillRequestDto skillRequest,
            SkillService skillService) =>
        {
            var idValidationError = RequestValidator.ValidatePositiveId(id, "Skill");
            if (idValidationError is not null)
            {
                return Results.BadRequest(idValidationError);
            }

            var validationError = RequestValidator.ValidateSkill(skillRequest);
            if (validationError is not null)
            {
                return Results.BadRequest(validationError);
            }

            var skill = await skillService.UpdateSkillAsync(id, skillRequest);
            return skill is null ? Results.NotFound() : Results.Ok(skill);
        })
        .WithName("UpdateSkill")
        .WithSummary("Update a skill")
        .WithDescription("Updates an existing portfolio skill when the provided positive skill ID exists.")
        .Produces<SkillDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapDelete("/skills/{id:int}", async ([FromRoute] int id, SkillService skillService) =>
        {
            var validationError = RequestValidator.ValidatePositiveId(id, "Skill");
            if (validationError is not null)
            {
                return Results.BadRequest(validationError);
            }

            var deleted = await skillService.DeleteSkillAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteSkill")
        .WithSummary("Delete a skill")
        .WithDescription("Deletes an existing portfolio skill when the provided positive skill ID exists.")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
