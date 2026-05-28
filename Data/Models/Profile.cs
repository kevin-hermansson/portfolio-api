namespace Portfolio.Api.Data.Models;

public class Profile
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Headline { get; set; } = string.Empty;

    public string Bio { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string? GitHubUrl { get; set; }

    public string? LinkedInUrl { get; set; }

    public string? WebsiteUrl { get; set; }

    public string? ResumeUrl { get; set; }
}
