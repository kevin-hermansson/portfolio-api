using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Core.DTOs;

public class ProjectRequestDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Url]
    [StringLength(2048)]
    public string RepositoryUrl { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public List<string> Technologies { get; set; } = new();
}
