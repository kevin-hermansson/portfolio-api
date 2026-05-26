using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.Data.Models;

namespace Portfolio.Api.Data.Repositories;

public class SkillRepository
{
    private readonly PortfolioDbContext _context;

    public SkillRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<List<Skill>> GetAllAsync()
    {
        return await _context.Skills.ToListAsync();
    }

    public async Task<Skill?> GetByIdAsync(int id)
    {
        return await _context.Skills.FindAsync(id);
    }

    public async Task<Skill> AddAsync(Skill skill)
    {
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        return skill;
    }

    public async Task<Skill?> UpdateAsync(int id, Skill updatedSkill)
    {
        var skill = await GetByIdAsync(id);

        if (skill is null)
        {
            return null;
        }

        skill.Name = updatedSkill.Name;
        skill.Category = updatedSkill.Category;
        skill.Level = updatedSkill.Level;

        await _context.SaveChangesAsync();

        return skill;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var skill = await GetByIdAsync(id);

        if (skill is null)
        {
            return false;
        }

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();

        return true;
    }
}
