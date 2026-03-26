namespace MarothiMohale.Portfolio.Web.Configuration;

public class PortfolioOptions
{
    public const string SectionName = "Portfolio";

    public string DefaultProjectImageUrl { get; set; } = "/images/project-custom.svg";
    public string DefaultProfileImageUrl { get; set; } = "/images/marothi-profile.jfif";
    public string? PersistentDataRootPath { get; set; }
    public int StartupMigrationMaxRetries { get; set; } = 3;
    public int StartupMigrationRetryDelaySeconds { get; set; } = 2;
}
