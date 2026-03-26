using System.ComponentModel.DataAnnotations;

namespace MarothiMohale.Portfolio.Web.Models;

public class BlogPost
{
    public int Id { get; set; }

    [Required, StringLength(180)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(180)]
    public string Slug { get; set; } = string.Empty;

    [Required, StringLength(320)]
    public string Excerpt { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime PublishedAtUtc { get; set; } = DateTime.UtcNow;

    public bool IsPublished { get; set; } = true;
}
