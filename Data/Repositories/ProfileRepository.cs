using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.Data.Models;

namespace Portfolio.Api.Data.Repositories;

public class ProfileRepository
{
    private readonly PortfolioDbContext _context;

    public ProfileRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<Profile?> GetAsync()
    {
        return await _context.Profiles.FirstOrDefaultAsync();
    }

    public async Task<Profile> UpsertAsync(Profile updatedProfile)
    {
        var profile = await GetAsync();

        if (profile is null)
        {
            _context.Profiles.Add(updatedProfile);
            await _context.SaveChangesAsync();
            return updatedProfile;
        }

        profile.FullName = updatedProfile.FullName;
        profile.Headline = updatedProfile.Headline;
        profile.Bio = updatedProfile.Bio;
        profile.Location = updatedProfile.Location;
        profile.Email = updatedProfile.Email;
        profile.Phone = updatedProfile.Phone;
        profile.GitHubUrl = updatedProfile.GitHubUrl;
        profile.LinkedInUrl = updatedProfile.LinkedInUrl;
        profile.WebsiteUrl = updatedProfile.WebsiteUrl;
        profile.ResumeUrl = updatedProfile.ResumeUrl;

        await _context.SaveChangesAsync();

        return profile;
    }

    public async Task<bool> DeleteAsync()
    {
        var profile = await GetAsync();

        if (profile is null)
        {
            return false;
        }

        _context.Profiles.Remove(profile);
        await _context.SaveChangesAsync();

        return true;
    }
}
