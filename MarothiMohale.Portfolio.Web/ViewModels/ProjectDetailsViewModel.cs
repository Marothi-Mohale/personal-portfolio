using MarothiMohale.Portfolio.Web.Models;

namespace MarothiMohale.Portfolio.Web.ViewModels;

public class ProjectDetailsViewModel
{
    public required Project Project { get; init; }
    public required IReadOnlyList<Project> RelatedProjects { get; init; }
    public required IReadOnlyList<SocialLink> SocialLinks { get; init; }
}
