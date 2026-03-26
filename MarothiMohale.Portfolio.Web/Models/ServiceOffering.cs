using System.ComponentModel.DataAnnotations;

namespace MarothiMohale.Portfolio.Web.Models;

public class ServiceOffering
{
    public int Id { get; set; }

    [Required, StringLength(140)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Range(0, 999)]
    public int DisplayOrder { get; set; }
}
