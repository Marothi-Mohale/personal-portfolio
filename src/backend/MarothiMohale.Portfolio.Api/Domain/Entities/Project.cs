using System.ComponentModel.DataAnnotations;

namespace MarothiMohale.Portfolio.Api.Domain.Entities;

public class Project
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(160)]
    public string Slug { get; set; } = string.Empty;

    [Required, StringLength(320)]
    public string ShortDescription { get; set; } = string.Empty;

    [Required]
    public string FullDescription { get; set; } = string.Empty;

    [Required, StringLength(300)]
    public string TechStack { get; set; } = string.Empty;

    [Url, StringLength(255)]
    public string? GitHubUrl { get; set; }

    [Url, StringLength(255)]
    public string? LiveUrl { get; set; }

    [StringLength(255)]
    public string? ImageUrl { get; set; }

    public bool IsFeatured { get; set; }

    [Range(0, 999)]
    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}