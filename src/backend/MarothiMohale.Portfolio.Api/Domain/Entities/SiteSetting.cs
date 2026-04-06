using System.ComponentModel.DataAnnotations;

namespace MarothiMohale.Portfolio.Api.Domain.Entities;

public class SiteSetting
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Key { get; set; } = string.Empty;

    [Required]
    public string Value { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}