using MarothiMohale.Portfolio.Web.Models;

namespace MarothiMohale.Portfolio.Web.ViewModels;

public class ProjectsPageViewModel
{
    public required Profile Profile { get; init; }
    public required IReadOnlyList<Project> Projects { get; init; }
}
