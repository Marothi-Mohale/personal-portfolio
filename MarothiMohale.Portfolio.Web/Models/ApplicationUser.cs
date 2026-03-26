using Microsoft.AspNetCore.Identity;

namespace MarothiMohale.Portfolio.Web.Models;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
}
