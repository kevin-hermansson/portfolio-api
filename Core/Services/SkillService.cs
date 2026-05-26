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

    public List<SkillDto> GetSkills()
    {
        var skills = _skillRepository.GetAll();
        var skillDtos = new List<SkillDto>();

        foreach (var skill in skills)
        {
            skillDtos.Add(ToDto(skill));
        }

        return skillDtos;
    }

    public SkillDto? GetSkill(int id)
    {
        var skill = _skillRepository.GetById(id);
        return skill is null ? null : ToDto(skill);
    }

    public SkillDto CreateSkill(SkillRequestDto skillRequest)
    {
        var skill = ToModel(skillRequest);
        var createdSkill = _skillRepository.Add(skill);

        return ToDto(createdSkill);
    }

    public SkillDto? UpdateSkill(int id, SkillRequestDto skillRequest)
    {
        var skill = ToModel(skillRequest);
        var updatedSkill = _skillRepository.Update(id, skill);

        return updatedSkill is null ? null : ToDto(updatedSkill);
    }

    public bool DeleteSkill(int id)
    {
        return _skillRepository.Delete(id);
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
