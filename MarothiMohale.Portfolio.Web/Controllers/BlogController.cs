using MarothiMohale.Portfolio.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarothiMohale.Portfolio.Web.Controllers;

public class BlogController : Controller
{
    private readonly IPortfolioService _portfolioService;

    public BlogController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Insights";
        ViewData["MetaDescription"] = "Short insights from Marothi Mohale on building product-ready software with .NET.";
        return View(await _portfolioService.GetBlogIndexAsync(cancellationToken));
    }

    [HttpGet("/insights/{slug}")]
    public async Task<IActionResult> Details(string slug, CancellationToken cancellationToken)
    {
        var model = await _portfolioService.GetBlogPostAsync(slug, cancellationToken);
        if (model is null)
        {
            return NotFound();
        }

        ViewData["Title"] = $"{model.Post.Title} | Insights";
        ViewData["MetaDescription"] = model.Post.Excerpt;
        return View(model);
    }
}
