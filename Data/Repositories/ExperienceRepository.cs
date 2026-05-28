using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Api.Data.Repositories;

public class ExperienceRepository
{
    private readonly PortfolioDbContext _context;

    public ExperienceRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<List<Experience>> GetAllAsync()
    {
        return await _context.Experiences.ToListAsync();
    }

    public async Task<Experience?> GetByIdAsync(int id)
    {
        return await _context.Experiences.FindAsync(id);
    }

    public async Task<Experience> AddAsync(Experience experience)
    {
        _context.Experiences.Add(experience);
        await _context.SaveChangesAsync();

        return experience;
    }

    public async Task<Experience?> UpdateAsync(int id, Experience updatedExperience)
    {
        var experience = await GetByIdAsync(id);

        if (experience is null)
        {
            return null;
        }

        experience.Company = updatedExperience.Company;
        experience.Role = updatedExperience.Role;
        experience.StartDate = updatedExperience.StartDate;
        experience.EndDate = updatedExperience.EndDate;
        experience.Description = updatedExperience.Description;
        experience.Technologies = updatedExperience.Technologies;

        await _context.SaveChangesAsync();

        return experience;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var experience = await GetByIdAsync(id);

        if (experience is null)
        {
            return false;
        }

        _context.Experiences.Remove(experience);
        await _context.SaveChangesAsync();

        return true;
    }
}
