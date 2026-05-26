using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Core.DTOs;

public class SkillRequestDto
{
    [Required]
    [StringLength(80, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Category { get; set; } = string.Empty;

    [Range(1, 5)]
    public int Level { get; set; }
}
