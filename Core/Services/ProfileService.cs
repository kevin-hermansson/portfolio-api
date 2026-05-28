using Portfolio.Api.Core.DTOs;
using Portfolio.Api.Data.Models;
using Portfolio.Api.Data.Repositories;

namespace Portfolio.Api.Core.Services;

public class ProfileService
{
    private readonly ProfileRepository _profileRepository;

    public ProfileService(ProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<ProfileDto?> GetProfileAsync()
    {
        var profile = await _profileRepository.GetAsync();
        return profile is null ? null : ToDto(profile);
    }

    public async Task<ProfileDto> UpsertProfileAsync(ProfileRequestDto profileRequest)
    {
        var profile = ToModel(profileRequest);
        var savedProfile = await _profileRepository.UpsertAsync(profile);

        return ToDto(savedProfile);
    }

    public async Task<bool> DeleteProfileAsync()
    {
        return await _profileRepository.DeleteAsync();
    }

    private static ProfileDto ToDto(Profile profile)
    {
        return new ProfileDto
        {
            Id = profile.Id,
            FullName = profile.FullName,
            Headline = profile.Headline,
            Bio = profile.Bio,
            Location = profile.Location,
            Email = profile.Email,
            Phone = profile.Phone,
            GitHubUrl = profile.GitHubUrl,
            LinkedInUrl = profile.LinkedInUrl,
            WebsiteUrl = profile.WebsiteUrl,
            ResumeUrl = profile.ResumeUrl
        };
    }

    private static Profile ToModel(ProfileRequestDto profileRequest)
    {
        return new Profile
        {
            FullName = profileRequest.FullName,
            Headline = profileRequest.Headline,
            Bio = profileRequest.Bio,
            Location = profileRequest.Location,
            Email = profileRequest.Email,
            Phone = profileRequest.Phone,
            GitHubUrl = profileRequest.GitHubUrl,
            LinkedInUrl = profileRequest.LinkedInUrl,
            WebsiteUrl = profileRequest.WebsiteUrl,
            ResumeUrl = profileRequest.ResumeUrl
        };
    }
}
