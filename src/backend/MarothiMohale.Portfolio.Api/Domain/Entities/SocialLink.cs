using System.ComponentModel.DataAnnotations;

namespace MarothiMohale.Portfolio.Api.Domain.Entities;

public class SocialLink
{
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Platform { get; set; } = string.Empty;

    [Required, Url, StringLength(255)]
    public string Url { get; set; } = string.Empty;

    [Range(0, 999)]
    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}