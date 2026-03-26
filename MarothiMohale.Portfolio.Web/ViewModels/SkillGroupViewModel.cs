using MarothiMohale.Portfolio.Web.Models;

namespace MarothiMohale.Portfolio.Web.ViewModels;

public class SkillGroupViewModel
{
    public required string Category { get; init; }
    public required IReadOnlyList<Skill> Skills { get; init; }
}
