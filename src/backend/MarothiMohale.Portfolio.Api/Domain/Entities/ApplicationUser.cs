using Microsoft.AspNetCore.Identity;

namespace MarothiMohale.Portfolio.Api.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}