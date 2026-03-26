using MarothiMohale.Portfolio.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarothiMohale.Portfolio.Web.Controllers;

public class HomeController : Controller
{
    private readonly IPortfolioService _portfolioService;

    public HomeController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Marothi Mohale | Software Developer";
        ViewData["MetaDescription"] = "Portfolio of Marothi Mohale, a software developer focused on C#, ASP.NET Core, web development, and data engineering.";
        return View(await _portfolioService.GetHomePageAsync(cancellationToken));
    }
}
