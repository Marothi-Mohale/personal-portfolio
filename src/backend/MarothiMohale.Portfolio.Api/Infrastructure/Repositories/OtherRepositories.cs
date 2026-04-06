using MarothiMohale.Portfolio.Api.Application.Interfaces;
using MarothiMohale.Portfolio.Api.Domain.Entities;
using MarothiMohale.Portfolio.Api.Infrastructure.Data;

namespace MarothiMohale.Portfolio.Api.Infrastructure.Repositories;

public class ExperienceRepository : Repository<Experience>, IExperienceRepository
{
    public ExperienceRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Experience>> GetOrderedAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Experiences
            .OrderBy(e => e.DisplayOrder)
            .ToListAsync(cancellationToken);
    }
}

public class ServiceRepository : Repository<Service>, IServiceRepository
{
    public ServiceRepository(ApplicationDbContext context) : base(context) { }
}

public class TestimonialRepository : Repository<Testimonial>, ITestimonialRepository
{
    public TestimonialRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Testimonial>> GetFeaturedAsync(int count = 3, CancellationToken cancellationToken = default)
    {
        return await _context.Testimonials
            .Where(t => t.IsFeatured)
            .OrderBy(t => t.DisplayOrder)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}

public class SocialLinkRepository : Repository<SocialLink>, ISocialLinkRepository
{
    public SocialLinkRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<SocialLink>> GetOrderedAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SocialLinks
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync(cancellationToken);
    }
}

public class ContactMessageRepository : Repository<ContactMessage>, IContactMessageRepository
{
    public ContactMessageRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<ContactMessage>> GetUnreviewedAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ContactMessages
            .Where(c => !c.IsReviewed)
            .OrderByDescending(c => c.SubmittedAtUtc)
            .ToListAsync(cancellationToken);
    }
}

public class SiteSettingRepository : Repository<SiteSetting>, ISiteSettingRepository
{
    public SiteSettingRepository(ApplicationDbContext context) : base(context) { }

    public async Task<string?> GetValueAsync(string key, CancellationToken cancellationToken = default)
    {
        var setting = await _context.SiteSettings
            .FirstOrDefaultAsync(s => s.Key == key, cancellationToken);
        return setting?.Value;
    }

    public async Task<SiteSetting?> GetByKeyAsync(string key, CancellationToken cancellationToken = default)
    {
        return await _context.SiteSettings
            .FirstOrDefaultAsync(s => s.Key == key, cancellationToken);
    }
}