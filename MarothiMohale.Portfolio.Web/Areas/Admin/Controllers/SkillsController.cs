using MarothiMohale.Portfolio.Web.Data;
using MarothiMohale.Portfolio.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarothiMohale.Portfolio.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class SkillsController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public SkillsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await _dbContext.Skills.OrderBy(x => x.Category).ThenBy(x => x.DisplayOrder).ToListAsync(cancellationToken));

    public IActionResult Create() => View(new Skill());

    [HttpPost]
    public async Task<IActionResult> Create(Skill skill, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(skill);
        }

        _dbContext.Skills.Add(skill);
        await _dbContext.SaveChangesAsync(cancellationToken);
        TempData["AdminMessage"] = "Skill created.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var skill = await _dbContext.Skills.FindAsync([id], cancellationToken);
        return skill is null ? NotFound() : View(skill);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Skill skill, CancellationToken cancellationToken)
    {
        if (id != skill.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(skill);
        }

        _dbContext.Update(skill);
        await _dbContext.SaveChangesAsync(cancellationToken);
        TempData["AdminMessage"] = "Skill updated.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var skill = await _dbContext.Skills.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return skill is null ? NotFound() : View(skill);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var skill = await _dbContext.Skills.FindAsync([id], cancellationToken);
        if (skill is not null)
        {
            _dbContext.Skills.Remove(skill);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        TempData["AdminMessage"] = "Skill deleted.";
        return RedirectToAction(nameof(Index));
    }
}
