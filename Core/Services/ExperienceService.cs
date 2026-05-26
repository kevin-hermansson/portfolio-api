using Portfolio.Api.Core.DTOs;
using Portfolio.Api.Data.Models;
using Portfolio.Api.Data.Repositories;

namespace Portfolio.Api.Core.Services;

public class ExperienceService
{
    private readonly ExperienceRepository _experienceRepository;

    public ExperienceService(ExperienceRepository experienceRepository)
    {
        _experienceRepository = experienceRepository;
    }

    public List<ExperienceDto> GetExperiences()
    {
        var experiences = _experienceRepository.GetAll();
        var experienceDtos = new List<ExperienceDto>();

        foreach (var experience in experiences)
        {
            experienceDtos.Add(ToDto(experience));
        }

        return experienceDtos;
    }

    public ExperienceDto? GetExperience(int id)
    {
        var experience = _experienceRepository.GetById(id);
        return experience is null ? null : ToDto(experience);
    }

    public ExperienceDto CreateExperience(ExperienceRequestDto experienceRequest)
    {
        var experience = ToModel(experienceRequest);
        var createdExperience = _experienceRepository.Add(experience);

        return ToDto(createdExperience);
    }

    public ExperienceDto? UpdateExperience(int id, ExperienceRequestDto experienceRequest)
    {
        var experience = ToModel(experienceRequest);
        var updatedExperience = _experienceRepository.Update(id, experience);

        return updatedExperience is null ? null : ToDto(updatedExperience);
    }

    public bool DeleteExperience(int id)
    {
        return _experienceRepository.Delete(id);
    }

    private static ExperienceDto ToDto(Experience experience)
    {
        return new ExperienceDto
        {
            Id = experience.Id,
            Company = experience.Company,
            Role = experience.Role,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Description = experience.Description,
            Technologies = experience.Technologies
        };
    }

    private static Experience ToModel(ExperienceRequestDto experienceRequest)
    {
        return new Experience
        {
            Company = experienceRequest.Company,
            Role = experienceRequest.Role,
            StartDate = experienceRequest.StartDate,
            EndDate = experienceRequest.EndDate,
            Description = experienceRequest.Description,
            Technologies = experienceRequest.Technologies
        };
    }
}
