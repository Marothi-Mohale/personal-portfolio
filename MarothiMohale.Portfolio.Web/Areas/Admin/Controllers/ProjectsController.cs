using MarothiMohale.Portfolio.Web.Data;
using MarothiMohale.Portfolio.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarothiMohale.Portfolio.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class ProjectsController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public ProjectsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await _dbContext.Projects.OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken));

    public IActionResult Create() => View(new Project { ImageUrl = "/images/project-custom.svg" });

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Title,Slug,ShortDescription,FullDescription,TechStack,GitHubUrl,LiveUrl,ImageUrl,IsFeatured,DisplayOrder")] Project project, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(project);
        }

        try
        {
            project.CreatedAt = DateTime.UtcNow;
            project.UpdatedAt = DateTime.UtcNow;
            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync(cancellationToken);
            TempData["AdminMessage"] = "Project created.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "The project could not be saved. Check the slug and links, then try again.");
            return View(project);
        }
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects.FindAsync([id], cancellationToken);
        return project is null ? NotFound() : View(project);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Slug,ShortDescription,FullDescription,TechStack,GitHubUrl,LiveUrl,ImageUrl,IsFeatured,DisplayOrder,CreatedAt")] Project project, CancellationToken cancellationToken)
    {
        if (id != project.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(project);
        }

        try
        {
            project.UpdatedAt = DateTime.UtcNow;
            _dbContext.Update(project);
            await _dbContext.SaveChangesAsync(cancellationToken);
            TempData["AdminMessage"] = "Project updated.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "The project could not be updated. Check the slug and links, then try again.");
            return View(project);
        }
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return project is null ? NotFound() : View(project);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects.FindAsync([id], cancellationToken);
        if (project is null)
        {
            return RedirectToAction(nameof(Index));
        }

        _dbContext.Projects.Remove(project);
        await _dbContext.SaveChangesAsync(cancellationToken);
        TempData["AdminMessage"] = "Project deleted.";
        return RedirectToAction(nameof(Index));
    }
}
