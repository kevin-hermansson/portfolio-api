namespace Portfolio.Api.Data.Models;

public class Experience
{
    public int Id { get; set; }

    public string Company { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string Description { get; set; } = string.Empty;

    public List<string> Technologies { get; set; } = new();
}
