using System.ComponentModel.DataAnnotations;

namespace MarothiMohale.Portfolio.Web.Models;

public class Profile
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Headline { get; set; } = string.Empty;

    [Required]
    public string ProfessionalSummary { get; set; } = string.Empty;

    [Required]
    public string About { get; set; } = string.Empty;

    [Required, StringLength(150), EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string Phone { get; set; } = string.Empty;

    [Required, StringLength(120)]
    public string Location { get; set; } = string.Empty;

    [Url, StringLength(255)]
    public string ResumeUrl { get; set; } = string.Empty;

    [StringLength(255)]
    public string HeroPrimaryCtaUrl { get; set; } = string.Empty;

    [StringLength(255)]
    public string HeroSecondaryCtaUrl { get; set; } = string.Empty;
}
