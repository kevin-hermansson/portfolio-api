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

    public List<ProjectDto> GetProjects()
    {
        var projects = _projectRepository.GetAll();
        var projectDtos = new List<ProjectDto>();

        foreach (var project in projects)
        {
            projectDtos.Add(ToDto(project));
        }

        return projectDtos;
    }

    public ProjectDto? GetProject(int id)
    {
        var project = _projectRepository.GetById(id);
        return project is null ? null : ToDto(project);
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
}
