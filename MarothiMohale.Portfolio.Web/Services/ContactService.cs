using MarothiMohale.Portfolio.Web.Data;
using MarothiMohale.Portfolio.Web.Models;
using MarothiMohale.Portfolio.Web.ViewModels;

namespace MarothiMohale.Portfolio.Web.Services;

public class ContactService : IContactService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<ContactService> _logger;

    public ContactService(ApplicationDbContext dbContext, ILogger<ContactService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> SubmitMessageAsync(ContactFormViewModel form, CancellationToken cancellationToken = default)
    {
        try
        {
            _dbContext.ContactMessages.Add(new ContactMessage
            {
                Name = form.Name.Trim(),
                Email = form.Email.Trim(),
                Phone = form.Phone?.Trim(),
                Subject = form.Subject.Trim(),
                Message = form.Message.Trim(),
                SubmittedAtUtc = DateTime.UtcNow
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save contact message for {Email}", form.Email);
            return false;
        }
    }
}
