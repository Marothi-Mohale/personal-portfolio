using MarothiMohale.Portfolio.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarothiMohale.Portfolio.Web.Controllers;

public class ProjectsController : Controller
{
    private readonly IPortfolioService _portfolioService;

    public ProjectsController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Projects";
        ViewData["MetaDescription"] = "Selected software projects by Marothi Mohale covering APIs, admin systems, front-end work, and learning products.";
        return View(await _portfolioService.GetProjectsPageAsync(cancellationToken));
    }

    [HttpGet("/projects/{slug}")]
    public async Task<IActionResult> Details(string slug, CancellationToken cancellationToken)
    {
        var model = await _portfolioService.GetProjectDetailsAsync(slug, cancellationToken);
        if (model is null)
        {
            return NotFound();
        }

        ViewData["Title"] = $"{model.Project.Title} | Project";
        ViewData["MetaDescription"] = model.Project.ShortDescription;
        return View(model);
    }
}
