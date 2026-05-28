using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.Data.Models;

namespace Portfolio.Api.Data.Seed;

public static class PortfolioDbSeeder
{
    public static async Task SeedAsync(PortfolioDbContext context)
    {
        await SeedProfileAsync(context);
        await SeedSkillsAsync(context);
        await SeedExperiencesAsync(context);
        await SeedProjectsAsync(context);
    }

    private static async Task SeedProfileAsync(PortfolioDbContext context)
    {
        if (await context.Profiles.AnyAsync())
        {
            return;
        }

        context.Profiles.Add(new Profile
        {
            FullName = "Kevin Hermansson",
            Headline = ".NET Cloud Developer-student",
            Bio = "Jag har nästan 10 års erfarenhet inom lager och transport, varav 6 år som teamleader på ICA. År 2023 valde jag att satsa fullt ut på mitt teknikintresse och började studera systemutveckling. Jag studerar nu till .NET Cloud Developer på IT-Högskolan i Göteborg. Jag är målmedveten, lösningsorienterad och drivs av ständig utveckling, att bygga saker och förbättra tekniska lösningar.",
            Location = "Göteborg",
            Email = "hermanssonkevin@gmail.com",
            Phone = "070 046 81 80",
            GitHubUrl = "https://github.com/kevin-hermansson",
            LinkedInUrl = "https://linkedin.com/in/kevinhermansson",
            WebsiteUrl = null,
            ResumeUrl = null
        });

        await context.SaveChangesAsync();
    }

    private static async Task SeedSkillsAsync(PortfolioDbContext context)
    {
        if (await context.Skills.AnyAsync())
        {
            return;
        }

        context.Skills.AddRange(
            new Skill { Name = "C#", Category = "Backend", Level = 5 },
            new Skill { Name = "ASP.NET Core Web API", Category = "Backend", Level = 4 },
            new Skill { Name = "REST API", Category = "Backend", Level = 4 },
            new Skill { Name = "Entity Framework Core", Category = "Databas", Level = 4 },
            new Skill { Name = "SQL", Category = "Databas", Level = 4 },
            new Skill { Name = "MongoDB", Category = "Databas", Level = 3 },
            new Skill { Name = "JavaScript", Category = "Frontend", Level = 3 },
            new Skill { Name = "TypeScript", Category = "Frontend", Level = 3 },
            new Skill { Name = "HTML", Category = "Frontend", Level = 4 },
            new Skill { Name = "CSS", Category = "Frontend", Level = 4 },
            new Skill { Name = "React", Category = "Frontend", Level = 3 },
            new Skill { Name = "Bootstrap", Category = "Frontend", Level = 3 },
            new Skill { Name = "Git", Category = "Verktyg", Level = 4 },
            new Skill { Name = "CI/CD", Category = "DevOps", Level = 3 },
            new Skill { Name = "Visual Studio", Category = "Verktyg", Level = 4 },
            new Skill { Name = "VS Code", Category = "Verktyg", Level = 4 },
            new Skill { Name = "Postman", Category = "Verktyg", Level = 4 },
            new Skill { Name = "Azure Portal", Category = "Moln", Level = 3 },
            new Skill { Name = "Docker", Category = "DevOps", Level = 3 }
        );

        await context.SaveChangesAsync();
    }

    private static async Task SeedExperiencesAsync(PortfolioDbContext context)
    {
        if (await context.Experiences.AnyAsync())
        {
            return;
        }

        context.Experiences.AddRange(
            new Experience
            {
                Company = "ICA Sverige AB, Centrallager Kungälv",
                Role = "Teamleader",
                StartDate = new DateOnly(2020, 1, 1),
                EndDate = null,
                Description = "Ledde och fördelade arbetet inom lager och produktion, ansvarade för planering och uppföljning av arbetsflöden, säkerställde kvalitet och leveranser enligt mål samt introducerade och handledde ny personal. Arbetade också med att förbättra interna processer.",
                Technologies = new List<string> { "Planering", "Uppföljning", "Introduktion av personal", "Processförbättring" }
            },
            new Experience
            {
                Company = "ICA Sverige AB, Centrallager Kungälv",
                Role = "Lagermedarbetare",
                StartDate = new DateOnly(2016, 1, 1),
                EndDate = new DateOnly(2020, 1, 1),
                Description = "Arbetade med lagerhantering, plock, pack och dagliga leveranser i en verksamhet med högt tempo och tydliga kvalitetskrav.",
                Technologies = new List<string> { "Lagerhantering", "Orderplock", "Packning", "Leveransflöden" }
            }
        );

        await context.SaveChangesAsync();
    }

    private static async Task SeedProjectsAsync(PortfolioDbContext context)
    {
        if (await context.Projects.AnyAsync())
        {
            return;
        }

        context.Projects.AddRange(
            new Project
            {
                Title = "Personlig portfölj",
                Description = "En enkel portföljlösning för att visa utbildning, erfarenhet, projekt och kontaktuppgifter på ett tydligt sätt.",
                RepositoryUrl = "https://github.com/kevin-hermansson/personlig-portfolio",
                Technologies = new List<string> { "C#", "ASP.NET Core Web API", "SQLite", "EF Core" }
            },
            new Project
            {
                Title = "Studieplanerare",
                Description = "Ett förslag till ett verktyg för att planera kurser, uppgifter och deadlines under studietiden.",
                RepositoryUrl = "https://github.com/kevin-hermansson/studieplanerare",
                Technologies = new List<string> { "C#", "REST API", "SQL", "Git" }
            },
            new Project
            {
                Title = "Lageröversikt",
                Description = "En enkel demo för att hålla koll på arbetsflöden och status i en lagerliknande miljö.",
                RepositoryUrl = "https://github.com/kevin-hermansson/lageroversikt",
                Technologies = new List<string> { "C#", "ASP.NET Core", "JavaScript", "SQL" }
            }
        );

        await context.SaveChangesAsync();
    }
}
