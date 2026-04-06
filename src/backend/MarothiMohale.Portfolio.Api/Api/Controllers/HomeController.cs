using MarothiMohale.Portfolio.Api.Application.DTOs.Responses;
using MarothiMohale.Portfolio.Api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarothiMohale.Portfolio.Api.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HomeController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;

    public HomeController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(HomeResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<HomeResponse>> GetHomeData(CancellationToken cancellationToken)
    {
        var result = await _portfolioService.GetHomeDataAsync(cancellationToken);
        return Ok(result);
    }
}