using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Core.DTOs;
using Portfolio.Api.Core.Services;
using Portfolio.Api.Core.Validation;

namespace Portfolio.Api.Endpoints;

public static class ProfileEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/profile", async (ProfileService profileService) =>
        {
            var profile = await profileService.GetProfileAsync();
            return profile is null ? Results.NotFound() : Results.Ok(profile);
        })
        .WithName("GetProfile")
        .WithSummary("Get the portfolio profile")
        .WithDescription("Returns the single profile record that holds the personal portfolio details.")
        .Produces<ProfileDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        app.MapPut("/profile", async (
            [FromBody] ProfileRequestDto profileRequest,
            ProfileService profileService) =>
        {
            var validationError = RequestValidator.ValidateProfile(profileRequest);
            if (validationError is not null)
            {
                return Results.BadRequest(validationError);
            }

            var profile = await profileService.UpsertProfileAsync(profileRequest);
            return Results.Ok(profile);
        })
        .WithName("UpsertProfile")
        .WithSummary("Create or update the portfolio profile")
        .WithDescription("Creates the profile when none exists, or updates the existing personal portfolio details.")
        .Produces<ProfileDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapDelete("/profile", async (ProfileService profileService) =>
        {
            var deleted = await profileService.DeleteProfileAsync();
            return deleted ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteProfile")
        .WithSummary("Delete the portfolio profile")
        .WithDescription("Deletes the single profile record when it exists.")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
