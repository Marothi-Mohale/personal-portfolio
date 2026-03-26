using MarothiMohale.Portfolio.Web.ViewModels;

namespace MarothiMohale.Portfolio.Web.Services;

public interface IAdminReferenceDataService
{
    Task<AdminDashboardViewModel> GetDashboardAsync(CancellationToken cancellationToken = default);
}
