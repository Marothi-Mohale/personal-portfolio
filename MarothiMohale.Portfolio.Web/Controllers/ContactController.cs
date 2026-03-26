using MarothiMohale.Portfolio.Web.Services;
using MarothiMohale.Portfolio.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MarothiMohale.Portfolio.Web.Controllers;

public class ContactController : Controller
{
    private readonly IPortfolioService _portfolioService;
    private readonly IContactService _contactService;

    public ContactController(IPortfolioService portfolioService, IContactService contactService)
    {
        _portfolioService = portfolioService;
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Contact";
        ViewData["MetaDescription"] = "Contact Marothi Mohale for software development, API delivery, data engineering, and technical training work.";
        return View(await _portfolioService.GetContactPageAsync(cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Index(ContactPageViewModel model, CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Contact";
        ViewData["MetaDescription"] = "Contact Marothi Mohale for software development, API delivery, data engineering, and technical training work.";

        if (!ModelState.IsValid)
        {
            var page = await _portfolioService.GetContactPageAsync(cancellationToken);
            return View(new ContactPageViewModel
            {
                Profile = page.Profile,
                SocialLinks = page.SocialLinks,
                Form = model.Form
            });
        }

        var success = await _contactService.SubmitMessageAsync(model.Form, cancellationToken);
        if (!success)
        {
            ModelState.AddModelError(string.Empty, "Something went wrong while sending your message. Please try again shortly.");
            var page = await _portfolioService.GetContactPageAsync(cancellationToken);
            return View(new ContactPageViewModel
            {
                Profile = page.Profile,
                SocialLinks = page.SocialLinks,
                Form = model.Form
            });
        }

        TempData["ContactSuccess"] = "Thanks for reaching out. Your message has been received.";
        return RedirectToAction(nameof(Index));
    }
}
