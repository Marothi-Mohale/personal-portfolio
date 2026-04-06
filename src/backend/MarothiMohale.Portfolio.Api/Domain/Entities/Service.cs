using System.ComponentModel.DataAnnotations;

namespace MarothiMohale.Portfolio.Api.Domain.Entities;

public class Service
{
    public int Id { get; set; }

    [Required, StringLength(140)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Range(0, 999)]
    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}