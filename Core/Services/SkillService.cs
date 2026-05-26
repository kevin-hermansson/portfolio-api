using Portfolio.Api.Core.DTOs;
using Portfolio.Api.Data.Models;
using Portfolio.Api.Data.Repositories;

namespace Portfolio.Api.Core.Services;

public class SkillService
{
    private readonly SkillRepository _skillRepository;

    public SkillService(SkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<List<SkillDto>> GetSkillsAsync()
    {
        var skills = await _skillRepository.GetAllAsync();
        var skillDtos = new List<SkillDto>();

        foreach (var skill in skills)
        {
            skillDtos.Add(ToDto(skill));
        }

        return skillDtos;
    }

    public async Task<SkillDto?> GetSkillAsync(int id)
    {
        var skill = await _skillRepository.GetByIdAsync(id);
        return skill is null ? null : ToDto(skill);
    }

    public async Task<SkillDto> CreateSkillAsync(SkillRequestDto skillRequest)
    {
        var skill = ToModel(skillRequest);
        var createdSkill = await _skillRepository.AddAsync(skill);

        return ToDto(createdSkill);
    }

    public async Task<SkillDto?> UpdateSkillAsync(int id, SkillRequestDto skillRequest)
    {
        var skill = ToModel(skillRequest);
        var updatedSkill = await _skillRepository.UpdateAsync(id, skill);

        return updatedSkill is null ? null : ToDto(updatedSkill);
    }

    public async Task<bool> DeleteSkillAsync(int id)
    {
        return await _skillRepository.DeleteAsync(id);
    }

    private static SkillDto ToDto(Skill skill)
    {
        return new SkillDto
        {
            Id = skill.Id,
            Name = skill.Name,
            Category = skill.Category,
            Level = skill.Level
        };
    }

    private static Skill ToModel(SkillRequestDto skillRequest)
    {
        return new Skill
        {
            Name = skillRequest.Name,
            Category = skillRequest.Category,
            Level = skillRequest.Level
        };
    }
}
