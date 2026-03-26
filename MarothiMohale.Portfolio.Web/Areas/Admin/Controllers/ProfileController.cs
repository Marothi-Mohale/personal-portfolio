using MarothiMohale.Portfolio.Web.Data;
using MarothiMohale.Portfolio.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarothiMohale.Portfolio.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class ProfileController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public ProfileController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Edit(CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles.OrderBy(x => x.Id).FirstOrDefaultAsync(cancellationToken);
        return profile is null ? NotFound() : View(profile);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([Bind("Id,FullName,Headline,ProfessionalSummary,About,Email,Phone,Location,ResumeUrl,HeroPrimaryCtaUrl,HeroSecondaryCtaUrl")] Profile profile, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(profile);
        }

        try
        {
            _dbContext.Update(profile);
            await _dbContext.SaveChangesAsync(cancellationToken);
            TempData["AdminMessage"] = "Profile updated.";
            return RedirectToAction(nameof(Edit));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "The profile could not be updated. Please try again.");
            return View(profile);
        }
    }
}
