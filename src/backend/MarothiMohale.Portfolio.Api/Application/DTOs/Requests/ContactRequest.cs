using System.ComponentModel.DataAnnotations;

namespace MarothiMohale.Portfolio.Api.Application.DTOs.Requests;

public class ContactRequest
{
    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [StringLength(40)]
    public string? Phone { get; set; }

    [Required, StringLength(160)]
    public string Subject { get; set; } = string.Empty;

    [Required]
    public string Message { get; set; } = string.Empty;
}