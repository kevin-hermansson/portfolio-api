using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Core.DTOs;

public class ExperienceRequestDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Company { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Role { get; set; } = string.Empty;

    [Required]
    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [Required]
    [StringLength(700, MinimumLength = 10)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public List<string> Technologies { get; set; } = new();
}
