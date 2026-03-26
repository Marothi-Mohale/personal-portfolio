using MarothiMohale.Portfolio.Web.Data;
using MarothiMohale.Portfolio.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarothiMohale.Portfolio.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class ExperiencesController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public ExperiencesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await _dbContext.Experiences.OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken));

    public IActionResult Create() => View(new Experience { StartDate = DateTime.UtcNow.Date });

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Title,Organization,Location,Summary,StartDate,EndDate,IsCurrent,DisplayOrder")] Experience experience, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(experience);
        }

        try
        {
            _dbContext.Experiences.Add(experience);
            await _dbContext.SaveChangesAsync(cancellationToken);
            TempData["AdminMessage"] = "Experience created.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "The experience could not be saved. Please try again.");
            return View(experience);
        }
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var experience = await _dbContext.Experiences.FindAsync([id], cancellationToken);
        return experience is null ? NotFound() : View(experience);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Organization,Location,Summary,StartDate,EndDate,IsCurrent,DisplayOrder")] Experience experience, CancellationToken cancellationToken)
    {
        if (id != experience.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(experience);
        }

        try
        {
            _dbContext.Update(experience);
            await _dbContext.SaveChangesAsync(cancellationToken);
            TempData["AdminMessage"] = "Experience updated.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "The experience could not be updated. Please try again.");
            return View(experience);
        }
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var experience = await _dbContext.Experiences.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return experience is null ? NotFound() : View(experience);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var experience = await _dbContext.Experiences.FindAsync([id], cancellationToken);
        if (experience is not null)
        {
            _dbContext.Experiences.Remove(experience);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        TempData["AdminMessage"] = "Experience deleted.";
        return RedirectToAction(nameof(Index));
    }
}
