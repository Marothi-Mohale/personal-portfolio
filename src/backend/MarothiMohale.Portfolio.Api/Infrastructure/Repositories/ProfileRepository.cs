using MarothiMohale.Portfolio.Api.Application.Interfaces;
using MarothiMohale.Portfolio.Api.Domain.Entities;
using MarothiMohale.Portfolio.Api.Infrastructure.Data;

namespace MarothiMohale.Portfolio.Api.Infrastructure.Repositories;

public class ProfileRepository : Repository<Profile>, IProfileRepository
{
    public ProfileRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Profile?> GetPrimaryProfileAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Profiles
            .OrderBy(p => p.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}