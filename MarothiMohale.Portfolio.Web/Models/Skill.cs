using System.ComponentModel.DataAnnotations;

namespace MarothiMohale.Portfolio.Web.Models;

public class Skill
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(80)]
    public string Category { get; set; } = string.Empty;

    [Range(1, 100)]
    public int ProficiencyPercent { get; set; }

    [Required]
    public string Summary { get; set; } = string.Empty;

    [Range(0, 999)]
    public int DisplayOrder { get; set; }
}
