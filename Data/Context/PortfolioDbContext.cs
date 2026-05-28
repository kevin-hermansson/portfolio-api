using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data.Models;

namespace Portfolio.Api.Data;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<Skill> Skills => Set<Skill>();

    public DbSet<Experience> Experiences => Set<Experience>();

    public DbSet<Profile> Profiles => Set<Profile>();
}
