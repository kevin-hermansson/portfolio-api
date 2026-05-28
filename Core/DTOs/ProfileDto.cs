using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Core.DTOs;

public class ProfileDto
{
    [Range(1, int.MaxValue)]
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string Headline { get; set; } = string.Empty;

    [Required]
    [StringLength(1000, MinimumLength = 10)]
    public string Bio { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Location { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Url]
    [StringLength(2048)]
    public string? GitHubUrl { get; set; }

    [Url]
    [StringLength(2048)]
    public string? LinkedInUrl { get; set; }

    [Url]
    [StringLength(2048)]
    public string? WebsiteUrl { get; set; }

    [Url]
    [StringLength(2048)]
    public string? ResumeUrl { get; set; }
}
