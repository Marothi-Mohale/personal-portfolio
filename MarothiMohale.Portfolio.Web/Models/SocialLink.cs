using System.ComponentModel.DataAnnotations;

namespace MarothiMohale.Portfolio.Web.Models;

public class SocialLink
{
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Platform { get; set; } = string.Empty;

    [Required, Url, StringLength(255)]
    public string Url { get; set; } = string.Empty;

    [Range(0, 999)]
    public int DisplayOrder { get; set; }
}
