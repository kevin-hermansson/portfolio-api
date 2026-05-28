using System.ComponentModel.DataAnnotations;
using Portfolio.Api.Core.DTOs;

namespace Portfolio.Api.Core.Validation;

public static class RequestValidator
{
    public static string? ValidateProject(ProjectRequestDto? request)
    {
        return ValidateRequest(request);
    }

    public static string? ValidateSkill(SkillRequestDto? request)
    {
        return ValidateRequest(request);
    }

    public static string? ValidateExperience(ExperienceRequestDto? request)
    {
        var validationError = ValidateRequest(request);
        if (validationError is not null)
        {
            return validationError;
        }

        if (request!.EndDate is not null && request.EndDate < request.StartDate)
        {
            return "End date must be after start date.";
        }

        return null;
    }

    public static string? ValidatePositiveId(int id, string resourceName)
    {
        return id < 1 ? $"{resourceName} id must be greater than zero." : null;
    }

    private static string? ValidateRequest<T>(T? request)
    {
        if (request is null)
        {
            return "Request body is required.";
        }

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(request);

        var isValid = Validator.TryValidateObject(
            request,
            validationContext,
            validationResults,
            validateAllProperties: true);

        if (isValid)
        {
            return null;
        }

        return validationResults.FirstOrDefault()?.ErrorMessage ?? "Invalid request.";
    }
}
