using MarothiMohale.Portfolio.Web.Data;
using MarothiMohale.Portfolio.Web.Models;
using MarothiMohale.Portfolio.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MarothiMohale.Portfolio.Web.Services;

public class PortfolioService : IPortfolioService
{
    private readonly ApplicationDbContext _dbContext;

    public PortfolioService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HomePageViewModel> GetHomePageAsync(CancellationToken cancellationToken = default)
    {
        var profile = await GetPrimaryProfileAsync(cancellationToken);
        var featuredProjects = await _dbContext.Projects.AsNoTracking().Where(x => x.IsFeatured).OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken);
        var skills = await _dbContext.Skills.AsNoTracking().OrderBy(x => x.Category).ThenByDescending(x => x.ProficiencyPercent).ThenBy(x => x.DisplayOrder).ToListAsync(cancellationToken);

        return new HomePageViewModel
        {
            Profile = profile,
            FeaturedProjects = featuredProjects,
            SkillGroups = skills.GroupBy(x => x.Category).Select(group => new SkillGroupViewModel
            {
                Category = group.Key,
                Skills = group.ToList()
            }).ToList(),
            Experiences = await _dbContext.Experiences.AsNoTracking().OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken),
            Services = await _dbContext.Services.AsNoTracking().OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken),
            Testimonials = await _dbContext.Testimonials.AsNoTracking().Where(x => x.IsFeatured).OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken),
            BlogPosts = await _dbContext.BlogPosts.AsNoTracking().Where(x => x.IsPublished).OrderByDescending(x => x.PublishedAtUtc).Take(3).ToListAsync(cancellationToken),
            SocialLinks = await _dbContext.SocialLinks.AsNoTracking().OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken)
        };
    }

    public async Task<ProjectsPageViewModel> GetProjectsPageAsync(CancellationToken cancellationToken = default)
    {
        return new ProjectsPageViewModel
        {
            Profile = await GetPrimaryProfileAsync(cancellationToken),
            Projects = await _dbContext.Projects.AsNoTracking().OrderBy(x => x.DisplayOrder).ThenByDescending(x => x.CreatedAt).ToListAsync(cancellationToken)
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
            RelatedProjects = await _dbContext.Projects.AsNoTracking().Where(x => x.Id != project.Id).OrderBy(x => x.DisplayOrder).Take(3).ToListAsync(cancellationToken),
            SocialLinks = await _dbContext.SocialLinks.AsNoTracking().OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken)
        };
    }

    public async Task<ContactPageViewModel> GetContactPageAsync(CancellationToken cancellationToken = default)
    {
        return new ContactPageViewModel
        {
            Profile = await GetPrimaryProfileAsync(cancellationToken),
            SocialLinks = await _dbContext.SocialLinks.AsNoTracking().OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken)
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
        return await _dbContext.Profiles.AsNoTracking().OrderBy(x => x.Id).FirstAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<SocialLink>> GetSocialLinksAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SocialLinks
            .AsNoTracking()
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync(cancellationToken);
    }
}
