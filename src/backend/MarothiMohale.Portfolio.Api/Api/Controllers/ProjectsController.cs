using MarothiMohale.Portfolio.Api.Application.DTOs.Responses;
using MarothiMohale.Portfolio.Api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarothiMohale.Portfolio.Api.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProjectSummaryResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProjectSummaryResponse>>> GetProjects(CancellationToken cancellationToken)
    {
        var projects = await _projectService.GetProjectsAsync(cancellationToken);
        return Ok(projects);
    }

    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(ProjectDetailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDetailResponse>> GetProject(string slug, CancellationToken cancellationToken)
    {
        var project = await _projectService.GetProjectBySlugAsync(slug, cancellationToken);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }
}