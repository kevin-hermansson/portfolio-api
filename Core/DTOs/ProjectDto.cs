namespace Portfolio.Api.Core.DTOs;

public class ProjectDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string RepositoryUrl { get; set; } = string.Empty;

    public List<string> Technologies { get; set; } = new();
}
