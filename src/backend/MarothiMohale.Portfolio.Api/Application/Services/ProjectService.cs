using AutoMapper;
using MarothiMohale.Portfolio.Api.Application.DTOs.Responses;
using MarothiMohale.Portfolio.Api.Application.Interfaces;

namespace MarothiMohale.Portfolio.Api.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProjectSummaryResponse>> GetProjectsAsync(CancellationToken cancellationToken = default)
    {
        var projects = await _projectRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ProjectSummaryResponse>>(
            projects.OrderBy(p => p.DisplayOrder).ThenByDescending(p => p.CreatedAt));
    }

    public async Task<ProjectDetailResponse?> GetProjectBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetBySlugAsync(slug, cancellationToken);
        return _mapper.Map<ProjectDetailResponse>(project);
    }
}