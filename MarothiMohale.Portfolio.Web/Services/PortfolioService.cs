using MarothiMohale.Portfolio.Web.Data;
using MarothiMohale.Portfolio.Web.Models;
using MarothiMohale.Portfolio.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MarothiMohale.Portfolio.Web.Configuration;

namespace MarothiMohale.Portfolio.Web.Services;

public class PortfolioService : IPortfolioService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly PortfolioOptions _options;

    public PortfolioService(ApplicationDbContext dbContext, IOptions<PortfolioOptions> options)
    {
        _dbContext = dbContext;
        _options = options.Value;
    }

    public async Task<HomePageViewModel> GetHomePageAsync(CancellationToken cancellationToken = default)
    {
        var profile = await GetPrimaryProfileAsync(cancellationToken);
        var featuredProjects = await _dbContext.Projects.AsNoTracking().Where(x => x.IsFeatured).OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken);
        var skills = await _dbContext.Skills.AsNoTracking().OrderBy(x => x.Category).ThenByDescending(x => x.ProficiencyPercent).ThenBy(x => x.DisplayOrder).ToListAsync(cancellationToken);

        return new HomePageViewModel
        {
            Profile = profile,
            FeaturedProjects = featuredProjects.Select(NormalizeProject).ToList(),
            SkillGroups = skills.GroupBy(x => x.Category).Select(group => new SkillGroupViewModel
            {
                Category = group.Key,
                Skills = group.ToList()
            }).ToList(),
            Experiences = await _dbContext.Experiences.AsNoTracking().OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken),
            Services = await _dbContext.Services.AsNoTracking().OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken),
            Testimonials = await _dbContext.Testimonials.AsNoTracking().Where(x => x.IsFeatured).OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken),
            BlogPosts = await _dbContext.BlogPosts.AsNoTracking().Where(x => x.IsPublished).OrderByDescending(x => x.PublishedAtUtc).Take(3).ToListAsync(cancellationToken),
            SocialLinks = await GetSocialLinksAsync(cancellationToken)
        };
    }

    public async Task<ProjectsPageViewModel> GetProjectsPageAsync(CancellationToken cancellationToken = default)
    {
        return new ProjectsPageViewModel
        {
            Profile = await GetPrimaryProfileAsync(cancellationToken),
            Projects = (await _dbContext.Projects.AsNoTracking().OrderBy(x => x.DisplayOrder).ThenByDescending(x => x.CreatedAt).ToListAsync(cancellationToken))
                .Select(NormalizeProject)
                .ToList()
        };
    }

    public async Task<ProjectDetailsViewModel?> GetProjectDetailsAsync(string slug, CancellationToken cancellationToken = default)
    {
        var project = await _dbContext.Projects.AsNoTracking().FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
        if (project is null)
        {
            return null;
        }

        return new ProjectDetailsViewModel
        {
            Project = project,
            RelatedProjects = (await _dbContext.Projects.AsNoTracking().Where(x => x.Id != project.Id).OrderBy(x => x.DisplayOrder).Take(3).ToListAsync(cancellationToken))
                .Select(NormalizeProject)
                .ToList(),
            SocialLinks = await GetSocialLinksAsync(cancellationToken)
        };
    }

    public async Task<ContactPageViewModel> GetContactPageAsync(CancellationToken cancellationToken = default)
    {
        return new ContactPageViewModel
        {
            Profile = await GetPrimaryProfileAsync(cancellationToken),
            SocialLinks = await GetSocialLinksAsync(cancellationToken)
        };
    }

    public async Task<BlogIndexViewModel> GetBlogIndexAsync(CancellationToken cancellationToken = default)
    {
        return new BlogIndexViewModel
        {
            Profile = await GetPrimaryProfileAsync(cancellationToken),
            Posts = await _dbContext.BlogPosts.AsNoTracking().Where(x => x.IsPublished).OrderByDescending(x => x.PublishedAtUtc).ToListAsync(cancellationToken)
        };
    }

    public async Task<BlogDetailsViewModel?> GetBlogPostAsync(string slug, CancellationToken cancellationToken = default)
    {
        var post = await _dbContext.BlogPosts.AsNoTracking().FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
        if (post is null)
        {
            return null;
        }

        return new BlogDetailsViewModel
        {
            Post = post,
            RelatedPosts = await _dbContext.BlogPosts.AsNoTracking().Where(x => x.Id != post.Id && x.IsPublished).OrderByDescending(x => x.PublishedAtUtc).Take(2).ToListAsync(cancellationToken)
        };
    }

    public async Task<Profile> GetPrimaryProfileAsync(CancellationToken cancellationToken = default)
    {
        var profile = await _dbContext.Profiles.AsNoTracking().OrderBy(x => x.Id).FirstOrDefaultAsync(cancellationToken);
        return profile ?? new Profile
        {
            FullName = "Marothi Mohale",
            Headline = "Software Developer",
            ProfessionalSummary = "Dependable .NET, web, and data-focused software delivery.",
            About = "Profile content is being prepared.",
            Email = string.Empty,
            Phone = string.Empty,
            Location = "South Africa",
            ResumeUrl = "#resume",
            HeroPrimaryCtaUrl = "/Projects",
            HeroSecondaryCtaUrl = "/Contact"
        };
    }

    public async Task<IReadOnlyList<SocialLink>> GetSocialLinksAsync(CancellationToken cancellationToken = default)
    {
        return (await _dbContext.SocialLinks
            .AsNoTracking()
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync(cancellationToken))
            .Where(x => !string.IsNullOrWhiteSpace(x.Platform) && !string.IsNullOrWhiteSpace(x.Url))
            .ToList();
    }

    private Project NormalizeProject(Project project)
    {
        project.ImageUrl = string.IsNullOrWhiteSpace(project.ImageUrl) ? _options.DefaultProjectImageUrl : project.ImageUrl;
        project.GitHubUrl = string.IsNullOrWhiteSpace(project.GitHubUrl) ? string.Empty : project.GitHubUrl;
        project.LiveUrl = string.IsNullOrWhiteSpace(project.LiveUrl) ? null : project.LiveUrl;
        project.ShortDescription = string.IsNullOrWhiteSpace(project.ShortDescription) ? "Project details coming soon." : project.ShortDescription;
        project.FullDescription = string.IsNullOrWhiteSpace(project.FullDescription) ? project.ShortDescription : project.FullDescription;
        project.TechStack = string.IsNullOrWhiteSpace(project.TechStack) ? "Technology details available on request" : project.TechStack;
        return project;
    }
}
