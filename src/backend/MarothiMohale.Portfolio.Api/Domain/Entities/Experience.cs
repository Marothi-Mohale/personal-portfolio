using System.ComponentModel.DataAnnotations;

namespace MarothiMohale.Portfolio.Api.Domain.Entities;

public class Experience
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string Organization { get; set; } = string.Empty;

    [StringLength(120)]
    public string? Location { get; set; }

    [Required]
    public string Summary { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsCurrent { get; set; }

    [Range(0, 999)]
    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}