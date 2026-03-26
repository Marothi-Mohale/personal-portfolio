using MarothiMohale.Portfolio.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarothiMohale.Portfolio.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class DashboardController : Controller
{
    private readonly IAdminReferenceDataService _adminReferenceDataService;

    public DashboardController(IAdminReferenceDataService adminReferenceDataService)
    {
        _adminReferenceDataService = adminReferenceDataService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Admin Dashboard";
        return View(await _adminReferenceDataService.GetDashboardAsync(cancellationToken));
    }
}
