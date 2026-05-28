using Portfolio.Api.Core.DTOs;
using Portfolio.Api.Data.Models;
using Portfolio.Api.Data.Repositories;

namespace Portfolio.Api.Core.Services;

public class ProjectService
{
    private readonly ProjectRepository _projectRepository;

    public ProjectService(ProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<List<ProjectDto>> GetProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();
        var projectDtos = new List<ProjectDto>();

        foreach (var project in projects)
        {
            projectDtos.Add(ToDto(project));
        }

        return projectDtos;
    }

    public async Task<ProjectDto?> GetProjectAsync(int id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        return project is null ? null : ToDto(project);
    }

    public async Task<ProjectDto> CreateProjectAsync(ProjectRequestDto projectRequest)
    {
        var project = ToModel(projectRequest);
        var createdProject = await _projectRepository.AddAsync(project);

        return ToDto(createdProject);
    }

    public async Task<ProjectDto?> UpdateProjectAsync(int id, ProjectRequestDto projectRequest)
    {
        var project = ToModel(projectRequest);
        var updatedProject = await _projectRepository.UpdateAsync(id, project);

        return updatedProject is null ? null : ToDto(updatedProject);
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        return await _projectRepository.DeleteAsync(id);
    }

    private static ProjectDto ToDto(Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            RepositoryUrl = project.RepositoryUrl,
            Technologies = project.Technologies
        };
    }

    private static Project ToModel(ProjectRequestDto projectRequest)
    {
        return new Project
        {
            Title = projectRequest.Title,
            Description = projectRequest.Description,
            RepositoryUrl = projectRequest.RepositoryUrl,
            Technologies = projectRequest.Technologies
        };
    }
}
