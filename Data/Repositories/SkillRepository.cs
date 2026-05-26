using Portfolio.Api.Data.Models;

namespace Portfolio.Api.Data.Repositories;

public class SkillRepository
{
    private readonly List<Skill> _skills = new()
    {
        new Skill
        {
            Id = 1,
            Name = "C#",
            Category = "Backend",
            Level = 4
        },
        new Skill
        {
            Id = 2,
            Name = "ASP.NET Core",
            Category = "Backend",
            Level = 4
        }
    };

    public List<Skill> GetAll()
    {
        return _skills;
    }

    public Skill? GetById(int id)
    {
        foreach (var skill in _skills)
        {
            if (skill.Id == id)
            {
                return skill;
            }
        }

        return null;
    }

    public Skill Add(Skill skill)
    {
        skill.Id = GetNextId();
        _skills.Add(skill);
        return skill;
    }

    public Skill? Update(int id, Skill updatedSkill)
    {
        var skill = GetById(id);

        if (skill is null)
        {
            return null;
        }

        skill.Name = updatedSkill.Name;
        skill.Category = updatedSkill.Category;
        skill.Level = updatedSkill.Level;

        return skill;
    }

    public bool Delete(int id)
    {
        var skill = GetById(id);

        if (skill is null)
        {
            return false;
        }

        _skills.Remove(skill);
        return true;
    }

    private int GetNextId()
    {
        var nextId = 1;

        foreach (var skill in _skills)
        {
            if (skill.Id >= nextId)
            {
                nextId = skill.Id + 1;
            }
        }

        return nextId;
    }
}
