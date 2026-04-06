namespace MarothiMohale.Portfolio.Api.Api.Configuration;

public class PortfolioOptions
{
    public const string SectionName = "Portfolio";
    public string? PersistentDataRootPath { get; set; }
    public int StartupMigrationMaxRetries { get; set; } = 5;
    public int StartupMigrationRetryDelaySeconds { get; set; } = 3;
}