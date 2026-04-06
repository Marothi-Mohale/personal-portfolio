using MarothiMohale.Portfolio.Api.Application.DTOs.Requests;
using MarothiMohale.Portfolio.Api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarothiMohale.Portfolio.Api.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SubmitContactMessage([FromBody] ContactRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _contactService.SubmitContactMessageAsync(request, cancellationToken);
        return Ok(new { message = "Contact message submitted successfully" });
    }
}