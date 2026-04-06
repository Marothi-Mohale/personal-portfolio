using MarothiMohale.Portfolio.Api.Application.Interfaces;
using MarothiMohale.Portfolio.Api.Domain.Entities;
using MarothiMohale.Portfolio.Api.Infrastructure.Data;

namespace MarothiMohale.Portfolio.Api.Infrastructure.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Project>> GetFeaturedProjectsAsync(int count = 6, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Where(p => p.IsFeatured)
            .OrderBy(p => p.DisplayOrder)
            .ThenByDescending(p => p.CreatedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .FirstOrDefaultAsync(p => p.Slug == slug, cancellationToken);
    }
}