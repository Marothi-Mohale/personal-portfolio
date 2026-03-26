using MarothiMohale.Portfolio.Web.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MarothiMohale.Portfolio.Web.Services;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly ApplicationDbContext _dbContext;

    public DatabaseHealthCheck(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Database.CanConnectAsync(cancellationToken)
            ? HealthCheckResult.Healthy("Database reachable")
            : HealthCheckResult.Unhealthy("Database not reachable");
    }
}
