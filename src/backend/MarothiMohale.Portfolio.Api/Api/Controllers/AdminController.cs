using MarothiMohale.Portfolio.Api.Application.DTOs.Responses;
using MarothiMohale.Portfolio.Api.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarothiMohale.Portfolio.Api.Api.Controllers;

[ApiController]
[Route("api/v1/admin")]
[Authorize] // TODO: Add JWT auth
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("contact")]
    [ProducesResponseType(typeof(IEnumerable<ContactMessageResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContactMessageResponse>>> GetContactMessages(
        [FromQuery] bool unreviewedOnly = false,
        CancellationToken cancellationToken = default)
    {
        var messages = await _adminService.GetContactMessagesAsync(unreviewedOnly, cancellationToken);
        return Ok(messages);
    }

    [HttpPut("contact/{id}/review")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkContactMessageReviewed(int id, CancellationToken cancellationToken)
    {
        await _adminService.MarkContactMessageReviewedAsync(id, cancellationToken);
        return NoContent();
    }
}