using MarothiMohale.Portfolio.Web.Models;

namespace MarothiMohale.Portfolio.Web.ViewModels;

public class HomePageViewModel
{
    public required Profile Profile { get; init; }
    public required IReadOnlyList<Project> FeaturedProjects { get; init; }
    public required IReadOnlyList<SkillGroupViewModel> SkillGroups { get; init; }
    public required IReadOnlyList<Experience> Experiences { get; init; }
    public required IReadOnlyList<ServiceOffering> Services { get; init; }
    public required IReadOnlyList<Testimonial> Testimonials { get; init; }
    public required IReadOnlyList<BlogPost> BlogPosts { get; init; }
    public required IReadOnlyList<SocialLink> SocialLinks { get; init; }
}
