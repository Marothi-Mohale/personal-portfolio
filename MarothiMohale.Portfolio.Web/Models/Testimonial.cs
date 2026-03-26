using System.ComponentModel.DataAnnotations;

namespace MarothiMohale.Portfolio.Web.Models;

public class Testimonial
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string AuthorName { get; set; } = string.Empty;

    [Required, StringLength(160)]
    public string AuthorRole { get; set; } = string.Empty;

    [StringLength(160)]
    public string Company { get; set; } = string.Empty;

    [Required]
    public string Quote { get; set; } = string.Empty;

    public bool IsFeatured { get; set; }

    [Range(0, 999)]
    public int DisplayOrder { get; set; }
}
