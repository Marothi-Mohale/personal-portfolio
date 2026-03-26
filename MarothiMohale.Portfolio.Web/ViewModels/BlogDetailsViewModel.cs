using MarothiMohale.Portfolio.Web.Models;

namespace MarothiMohale.Portfolio.Web.ViewModels;

public class BlogDetailsViewModel
{
    public required BlogPost Post { get; init; }
    public required IReadOnlyList<BlogPost> RelatedPosts { get; init; }
}
