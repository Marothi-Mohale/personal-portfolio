using System.ComponentModel.DataAnnotations;

namespace MarothiMohale.Portfolio.Api.Domain.Entities;

public class Testimonial
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string AuthorName { get; set; } = string.Empty;

    [Required, StringLength(160)]
    public string AuthorRole { get; set; } = string.Empty;

    [StringLength(160)]
    public string? Company { get; set; }

    [Required]
    public string Quote { get; set; } = string.Empty;

    public bool IsFeatured { get; set; }

    [Range(0, 999)]
    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}