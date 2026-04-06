using AutoMapper;
using MarothiMohale.Portfolio.Api.Application.DTOs.Responses;
using MarothiMohale.Portfolio.Api.Application.Interfaces;

namespace MarothiMohale.Portfolio.Api.Application.Services;

public class AdminService : IAdminService
{
    private readonly IContactMessageRepository _contactMessageRepository;
    private readonly IMapper _mapper;

    public AdminService(IContactMessageRepository contactMessageRepository, IMapper mapper)
    {
        _contactMessageRepository = contactMessageRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ContactMessageResponse>> GetContactMessagesAsync(bool onlyUnreviewed = false, CancellationToken cancellationToken = default)
    {
        var messages = onlyUnreviewed
            ? await _contactMessageRepository.GetUnreviewedAsync(cancellationToken)
            : await _contactMessageRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<IEnumerable<ContactMessageResponse>>(
            messages.OrderByDescending(m => m.SubmittedAtUtc));
    }

    public async Task MarkContactMessageReviewedAsync(int id, CancellationToken cancellationToken = default)
    {
        var message = await _contactMessageRepository.GetByIdAsync(id, cancellationToken);
        if (message != null)
        {
            message.IsReviewed = true;
            message.UpdatedAt = DateTime.UtcNow;
            await _contactMessageRepository.UpdateAsync(message, cancellationToken);
        }
    }
}