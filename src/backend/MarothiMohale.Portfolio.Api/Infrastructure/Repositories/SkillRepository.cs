using MarothiMohale.Portfolio.Api.Application.Interfaces;
using MarothiMohale.Portfolio.Api.Domain.Entities;
using MarothiMohale.Portfolio.Api.Infrastructure.Data;

namespace MarothiMohale.Portfolio.Api.Infrastructure.Repositories;

public class SkillRepository : Repository<Skill>, ISkillRepository
{
    public SkillRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Skill>> GetGroupedByCategoryAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Skills
            .OrderBy(s => s.Category)
            .ThenByDescending(s => s.ProficiencyPercent)
            .ThenBy(s => s.DisplayOrder)
            .ToListAsync(cancellationToken);
    }
}