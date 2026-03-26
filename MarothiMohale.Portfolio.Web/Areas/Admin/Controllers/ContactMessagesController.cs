using MarothiMohale.Portfolio.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarothiMohale.Portfolio.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class ContactMessagesController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public ContactMessagesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await _dbContext.ContactMessages.OrderByDescending(x => x.SubmittedAtUtc).ToListAsync(cancellationToken));

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var message = await _dbContext.ContactMessages.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (message is null)
        {
            return NotFound();
        }

        if (!message.IsReviewed)
        {
            message.IsReviewed = true;
            await _dbContext.SaveChangesAsync(cancellationToken);
            TempData["AdminMessage"] = "Message marked as reviewed.";
        }

        return View(message);
    }
}
