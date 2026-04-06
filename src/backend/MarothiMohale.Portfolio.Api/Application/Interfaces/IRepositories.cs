using MarothiMohale.Portfolio.Api.Domain.Entities;

namespace MarothiMohale.Portfolio.Api.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}

public interface IProfileRepository : IRepository<Profile>
{
    Task<Profile?> GetPrimaryProfileAsync(CancellationToken cancellationToken = default);
}

public interface IProjectRepository : IRepository<Project>
{
    Task<IEnumerable<Project>> GetFeaturedProjectsAsync(int count = 6, CancellationToken cancellationToken = default);
    Task<Project?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
}

public interface ISkillRepository : IRepository<Skill>
{
    Task<IEnumerable<Skill>> GetGroupedByCategoryAsync(CancellationToken cancellationToken = default);
}

public interface IServiceRepository : IRepository<Service> { }

public interface IExperienceRepository : IRepository<Experience>
{
    Task<IEnumerable<Experience>> GetOrderedAsync(CancellationToken cancellationToken = default);
}

public interface ITestimonialRepository : IRepository<Testimonial>
{
    Task<IEnumerable<Testimonial>> GetFeaturedAsync(int count = 3, CancellationToken cancellationToken = default);
}

public interface ISocialLinkRepository : IRepository<SocialLink>
{
    Task<IEnumerable<SocialLink>> GetOrderedAsync(CancellationToken cancellationToken = default);
}

public interface IContactMessageRepository : IRepository<ContactMessage>
{
    Task<IEnumerable<ContactMessage>> GetUnreviewedAsync(CancellationToken cancellationToken = default);
}

public interface ISiteSettingRepository : IRepository<SiteSetting>
{
    Task<string?> GetValueAsync(string key, CancellationToken cancellationToken = default);
    Task<SiteSetting?> GetByKeyAsync(string key, CancellationToken cancellationToken = default);
}