using MarothiMohale.Portfolio.Web.Data;
using MarothiMohale.Portfolio.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MarothiMohale.Portfolio.Web.Services;

public class AdminReferenceDataService : IAdminReferenceDataService
{
    private readonly ApplicationDbContext _dbContext;

    public AdminReferenceDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AdminDashboardViewModel> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        return new AdminDashboardViewModel
        {
            ProjectsCount = await _dbContext.Projects.CountAsync(cancellationToken),
            SkillsCount = await _dbContext.Skills.CountAsync(cancellationToken),
            ServicesCount = await _dbContext.Services.CountAsync(cancellationToken),
            ExperiencesCount = await _dbContext.Experiences.CountAsync(cancellationToken),
            TestimonialsCount = await _dbContext.Testimonials.CountAsync(cancellationToken),
            ContactMessagesCount = await _dbContext.ContactMessages.CountAsync(cancellationToken)
        };
    }
}
