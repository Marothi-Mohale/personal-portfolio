using MarothiMohale.Portfolio.Web.Data;
using MarothiMohale.Portfolio.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarothiMohale.Portfolio.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class TestimonialsController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public TestimonialsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await _dbContext.Testimonials.OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken));

    public IActionResult Create() => View(new Testimonial());

    [HttpPost]
    public async Task<IActionResult> Create([Bind("AuthorName,AuthorRole,Company,Quote,IsFeatured,DisplayOrder")] Testimonial testimonial, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(testimonial);
        }

        try
        {
            _dbContext.Testimonials.Add(testimonial);
            await _dbContext.SaveChangesAsync(cancellationToken);
            TempData["AdminMessage"] = "Testimonial created.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "The testimonial could not be saved. Please try again.");
            return View(testimonial);
        }
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var testimonial = await _dbContext.Testimonials.FindAsync([id], cancellationToken);
        return testimonial is null ? NotFound() : View(testimonial);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,AuthorName,AuthorRole,Company,Quote,IsFeatured,DisplayOrder")] Testimonial testimonial, CancellationToken cancellationToken)
    {
        if (id != testimonial.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(testimonial);
        }

        try
        {
            _dbContext.Update(testimonial);
            await _dbContext.SaveChangesAsync(cancellationToken);
            TempData["AdminMessage"] = "Testimonial updated.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "The testimonial could not be updated. Please try again.");
            return View(testimonial);
        }
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var testimonial = await _dbContext.Testimonials.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return testimonial is null ? NotFound() : View(testimonial);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var testimonial = await _dbContext.Testimonials.FindAsync([id], cancellationToken);
        if (testimonial is not null)
        {
            _dbContext.Testimonials.Remove(testimonial);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        TempData["AdminMessage"] = "Testimonial deleted.";
        return RedirectToAction(nameof(Index));
    }
}
