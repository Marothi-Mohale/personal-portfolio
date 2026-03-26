using MarothiMohale.Portfolio.Web.Models;

namespace MarothiMohale.Portfolio.Web.ViewModels;

public class ContactPageViewModel
{
    public required Profile Profile { get; init; }
    public required IReadOnlyList<SocialLink> SocialLinks { get; init; }
    public ContactFormViewModel Form { get; init; } = new();
}
