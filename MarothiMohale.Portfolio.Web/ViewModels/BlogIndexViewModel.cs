using MarothiMohale.Portfolio.Web.Models;

namespace MarothiMohale.Portfolio.Web.ViewModels;

public class BlogIndexViewModel
{
    public required Profile Profile { get; init; }
    public required IReadOnlyList<BlogPost> Posts { get; init; }
}
