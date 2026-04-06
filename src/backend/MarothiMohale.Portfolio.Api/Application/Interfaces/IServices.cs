using MarothiMohale.Portfolio.Api.Application.DTOs;

namespace MarothiMohale.Portfolio.Api.Application.Interfaces;

public interface IPortfolioService
{
    Task<HomeResponse> GetHomeDataAsync(CancellationToken cancellationToken = default);
    Task<ProfileResponse?> GetProfileAsync(CancellationToken cancellationToken = default);
}

public interface IProjectService
{
    Task<IEnumerable<ProjectSummaryResponse>> GetProjectsAsync(CancellationToken cancellationToken = default);
    Task<ProjectDetailResponse?> GetProjectBySlugAsync(string slug, CancellationToken cancellationToken = default);
}

public interface IContactService
{
    Task SubmitContactMessageAsync(ContactRequest request, CancellationToken cancellationToken = default);
}

public interface IAdminService
{
    // CRUD operations for admin
    Task<IEnumerable<ContactMessageResponse>> GetContactMessagesAsync(bool onlyUnreviewed = false, CancellationToken cancellationToken = default);
    Task MarkContactMessageReviewedAsync(int id, CancellationToken cancellationToken = default);
}