using MarothiMohale.Portfolio.Web.Models;
using MarothiMohale.Portfolio.Web.ViewModels;

namespace MarothiMohale.Portfolio.Web.Services;

public interface IPortfolioService
{
    Task<HomePageViewModel> GetHomePageAsync(CancellationToken cancellationToken = default);
    Task<ProjectsPageViewModel> GetProjectsPageAsync(CancellationToken cancellationToken = default);
    Task<ProjectDetailsViewModel?> GetProjectDetailsAsync(string slug, CancellationToken cancellationToken = default);
    Task<ContactPageViewModel> GetContactPageAsync(CancellationToken cancellationToken = default);
    Task<BlogIndexViewModel> GetBlogIndexAsync(CancellationToken cancellationToken = default);
    Task<BlogDetailsViewModel?> GetBlogPostAsync(string slug, CancellationToken cancellationToken = default);
    Task<Profile> GetPrimaryProfileAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SocialLink>> GetSocialLinksAsync(CancellationToken cancellationToken = default);
}
