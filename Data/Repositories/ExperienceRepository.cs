using Portfolio.Api.Data.Models;

namespace Portfolio.Api.Data.Repositories;

public class ExperienceRepository
{
    private readonly List<Experience> _experiences = new()
    {
        new Experience
        {
            Id = 1,
            Company = "Example Company",
            Role = "Backend Developer",
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = null,
            Description = "Building backend APIs and portfolio services.",
            Technologies = new List<string> { "C#", "ASP.NET Core", "Minimal API" }
        }
    };

    public List<Experience> GetAll()
    {
        return _experiences;
    }

    public Experience? GetById(int id)
    {
        foreach (var experience in _experiences)
        {
            if (experience.Id == id)
            {
                return experience;
            }
        }

        return null;
    }

    public Experience Add(Experience experience)
    {
        experience.Id = GetNextId();
        _experiences.Add(experience);
        return experience;
    }

    public Experience? Update(int id, Experience updatedExperience)
    {
        var experience = GetById(id);

        if (experience is null)
        {
            return null;
        }

        experience.Company = updatedExperience.Company;
        experience.Role = updatedExperience.Role;
        experience.StartDate = updatedExperience.StartDate;
        experience.EndDate = updatedExperience.EndDate;
        experience.Description = updatedExperience.Description;
        experience.Technologies = updatedExperience.Technologies;

        return experience;
    }

    public bool Delete(int id)
    {
        var experience = GetById(id);

        if (experience is null)
        {
            return false;
        }

        _experiences.Remove(experience);
        return true;
    }

    private int GetNextId()
    {
        var nextId = 1;

        foreach (var experience in _experiences)
        {
            if (experience.Id >= nextId)
            {
                nextId = experience.Id + 1;
            }
        }

        return nextId;
    }
}
