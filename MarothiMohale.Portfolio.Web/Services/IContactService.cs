using MarothiMohale.Portfolio.Web.ViewModels;

namespace MarothiMohale.Portfolio.Web.Services;

public interface IContactService
{
    Task<bool> SubmitMessageAsync(ContactFormViewModel form, CancellationToken cancellationToken = default);
}
