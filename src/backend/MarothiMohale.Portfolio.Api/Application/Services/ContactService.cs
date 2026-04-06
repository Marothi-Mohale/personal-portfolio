using MarothiMohale.Portfolio.Api.Application.DTOs.Requests;
using MarothiMohale.Portfolio.Api.Application.Interfaces;
using MarothiMohale.Portfolio.Api.Domain.Entities;

namespace MarothiMohale.Portfolio.Api.Application.Services;

public class ContactService : IContactService
{
    private readonly IContactMessageRepository _contactMessageRepository;

    public ContactService(IContactMessageRepository contactMessageRepository)
    {
        _contactMessageRepository = contactMessageRepository;
    }

    public async Task SubmitContactMessageAsync(ContactRequest request, CancellationToken cancellationToken = default)
    {
        var contactMessage = new ContactMessage
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            Subject = request.Subject,
            Message = request.Message,
            SubmittedAtUtc = DateTime.UtcNow,
            IsReviewed = false
        };

        await _contactMessageRepository.AddAsync(contactMessage, cancellationToken);
    }
}