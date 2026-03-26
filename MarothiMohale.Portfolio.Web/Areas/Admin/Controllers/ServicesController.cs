using MarothiMohale.Portfolio.Web.Data;
using MarothiMohale.Portfolio.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarothiMohale.Portfolio.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class ServicesController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public ServicesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await _dbContext.Services.OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken));

    public IActionResult Create() => View(new ServiceOffering());

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Title,Description,DisplayOrder")] ServiceOffering service, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(service);
        }

        try
        {
            _dbContext.Services.Add(service);
            await _dbContext.SaveChangesAsync(cancellationToken);
            TempData["AdminMessage"] = "Service created.";
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "The service could not be saved. Please try again.");
            return View(service);
        }
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var service = await _dbContext.Services.FindAsync([id], cancellationToken);
        return service is null ? NotFound() : View(service);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DisplayOrder")] ServiceOffering service, CancellationToken cancellationToken)
    {
        if (id != service.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(service);
        }

        try
        {
            _dbContext.Update(service);
            await _dbContext.SaveChangesAsync(cancellationToken);
            TempData["AdminMessage"] = "Service updated.";
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "The service could not be updated. Please try again.");
            return View(service);
        }
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var service = await _dbContext.Services.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return service is null ? NotFound() : View(service);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var service = await _dbContext.Services.FindAsync([id], cancellationToken);
        if (service is null)
        {
            TempData["AdminMessage"] = "That service was already removed.";
            return RedirectToAction(nameof(Index));
        }

        _dbContext.Services.Remove(service);
        await _dbContext.SaveChangesAsync(cancellationToken);
        TempData["AdminMessage"] = "Service deleted.";
        return RedirectToAction(nameof(Index));
    }
}
