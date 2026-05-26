using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Core.DTOs;
using Portfolio.Api.Core.Services;

namespace Portfolio.Api.Endpoints;

public static class SkillEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/skills", (SkillService skillService) =>
        {
            var skills = skillService.GetSkills();
            return Results.Ok(skills);
        })
        .WithName("GetSkills")
        .WithSummary("Get all skills")
        .WithDescription("Returns every portfolio skill, including category and skill level.")
        .Produces<List<SkillDto>>(StatusCodes.Status200OK);

        app.MapGet("/skills/{id:int}", ([FromRoute] int id, SkillService skillService) =>
        {
            if (id < 1)
            {
                return Results.BadRequest("Skill id must be greater than zero.");
            }

            var skill = skillService.GetSkill(id);
            return skill is null ? Results.NotFound() : Results.Ok(skill);
        })
        .WithName("GetSkillById")
        .WithSummary("Get a skill by ID")
        .WithDescription("Returns a single portfolio skill when the provided positive skill ID exists.")
        .Produces<SkillDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapPost("/skills", (
            [FromBody] SkillRequestDto skillRequest,
            SkillService skillService) =>
        {
            if (string.IsNullOrWhiteSpace(skillRequest.Name) || skillRequest.Name.Length < 2 || skillRequest.Name.Length > 80)
            {
                return Results.BadRequest("Name must be between 2 and 80 characters.");
            }

            if (string.IsNullOrWhiteSpace(skillRequest.Category) || skillRequest.Category.Length < 2 || skillRequest.Category.Length > 50)
            {
                return Results.BadRequest("Category must be between 2 and 50 characters.");
            }

            if (skillRequest.Level < 1 || skillRequest.Level > 5)
            {
                return Results.BadRequest("Level must be between 1 and 5.");
            }

            var skill = skillService.CreateSkill(skillRequest);
            return Results.Created($"/skills/{skill.Id}", skill);
        })
        .WithName("CreateSkill")
        .WithSummary("Create a skill")
        .WithDescription("Creates a new portfolio skill from the provided skill details.")
        .Produces<SkillDto>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapPut("/skills/{id:int}", (
            [FromRoute] int id,
            [FromBody] SkillRequestDto skillRequest,
            SkillService skillService) =>
        {
            if (id < 1)
            {
                return Results.BadRequest("Skill id must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(skillRequest.Name) || skillRequest.Name.Length < 2 || skillRequest.Name.Length > 80)
            {
                return Results.BadRequest("Name must be between 2 and 80 characters.");
            }

            if (string.IsNullOrWhiteSpace(skillRequest.Category) || skillRequest.Category.Length < 2 || skillRequest.Category.Length > 50)
            {
                return Results.BadRequest("Category must be between 2 and 50 characters.");
            }

            if (skillRequest.Level < 1 || skillRequest.Level > 5)
            {
                return Results.BadRequest("Level must be between 1 and 5.");
            }

            var skill = skillService.UpdateSkill(id, skillRequest);
            return skill is null ? Results.NotFound() : Results.Ok(skill);
        })
        .WithName("UpdateSkill")
        .WithSummary("Update a skill")
        .WithDescription("Updates an existing portfolio skill when the provided positive skill ID exists.")
        .Produces<SkillDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapDelete("/skills/{id:int}", ([FromRoute] int id, SkillService skillService) =>
        {
            if (id < 1)
            {
                return Results.BadRequest("Skill id must be greater than zero.");
            }

            var deleted = skillService.DeleteSkill(id);
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
