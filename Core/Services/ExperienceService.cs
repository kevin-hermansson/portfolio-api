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

    public async Task<List<ExperienceDto>> GetExperiencesAsync()
    {
        var experiences = await _experienceRepository.GetAllAsync();
        var experienceDtos = new List<ExperienceDto>();

        foreach (var experience in experiences)
        {
            experienceDtos.Add(ToDto(experience));
        }

        return experienceDtos;
    }

    public async Task<ExperienceDto?> GetExperienceAsync(int id)
    {
        var experience = await _experienceRepository.GetByIdAsync(id);
        return experience is null ? null : ToDto(experience);
    }

    public async Task<ExperienceDto> CreateExperienceAsync(ExperienceRequestDto experienceRequest)
    {
        var experience = ToModel(experienceRequest);
        var createdExperience = await _experienceRepository.AddAsync(experience);

        return ToDto(createdExperience);
    }

    public async Task<ExperienceDto?> UpdateExperienceAsync(int id, ExperienceRequestDto experienceRequest)
    {
        var experience = ToModel(experienceRequest);
        var updatedExperience = await _experienceRepository.UpdateAsync(id, experience);

        return updatedExperience is null ? null : ToDto(updatedExperience);
    }

    public async Task<bool> DeleteExperienceAsync(int id)
    {
        return await _experienceRepository.DeleteAsync(id);
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
