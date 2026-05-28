using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Api.Data.Repositories;

public class ProjectRepository
{
    private readonly PortfolioDbContext _context;

    public ProjectRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<Project> AddAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return project;
    }

    public async Task<Project?> UpdateAsync(int id, Project updatedProject)
    {
        var project = await GetByIdAsync(id);

        if (project is null)
        {
            return null;
        }

        project.Title = updatedProject.Title;
        project.Description = updatedProject.Description;
        project.RepositoryUrl = updatedProject.RepositoryUrl;
        project.Technologies = updatedProject.Technologies;

        await _context.SaveChangesAsync();

        return project;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await GetByIdAsync(id);

        if (project is null)
        {
            return false;
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return true;
    }
}
