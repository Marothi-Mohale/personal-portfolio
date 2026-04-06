using AutoMapper;
using MarothiMohale.Portfolio.Api.Application.DTOs.Responses;
using MarothiMohale.Portfolio.Api.Application.Interfaces;

namespace MarothiMohale.Portfolio.Api.Application.Services;

public class PortfolioService : IPortfolioService
{
    private readonly IProfileRepository _profileRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IExperienceRepository _experienceRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly ITestimonialRepository _testimonialRepository;
    private readonly ISocialLinkRepository _socialLinkRepository;
    private readonly IMapper _mapper;

    public PortfolioService(
        IProfileRepository profileRepository,
        IProjectRepository projectRepository,
        ISkillRepository skillRepository,
        IExperienceRepository experienceRepository,
        IServiceRepository serviceRepository,
        ITestimonialRepository testimonialRepository,
        ISocialLinkRepository socialLinkRepository,
        IMapper mapper)
    {
        _profileRepository = profileRepository;
        _projectRepository = projectRepository;
        _skillRepository = skillRepository;
        _experienceRepository = experienceRepository;
        _serviceRepository = serviceRepository;
        _testimonialRepository = testimonialRepository;
        _socialLinkRepository = socialLinkRepository;
        _mapper = mapper;
    }

    public async Task<HomeResponse> GetHomeDataAsync(CancellationToken cancellationToken = default)
    {
        var profile = await _profileRepository.GetPrimaryProfileAsync(cancellationToken);
        var featuredProjects = await _projectRepository.GetFeaturedProjectsAsync(6, cancellationToken);
        var skills = await _skillRepository.GetGroupedByCategoryAsync(cancellationToken);
        var experiences = await _experienceRepository.GetOrderedAsync(cancellationToken);
        var services = await _serviceRepository.GetAllAsync(cancellationToken);
        var testimonials = await _testimonialRepository.GetFeaturedAsync(3, cancellationToken);
        var socialLinks = await _socialLinkRepository.GetOrderedAsync(cancellationToken);

        var skillGroups = skills
            .GroupBy(s => s.Category)
            .Select(g => new SkillGroupResponse
            {
                Category = g.Key,
                Skills = _mapper.Map<IEnumerable<SkillResponse>>(g)
            });

        return new HomeResponse
        {
            Profile = _mapper.Map<ProfileResponse>(profile),
            FeaturedProjects = _mapper.Map<IEnumerable<ProjectSummaryResponse>>(featuredProjects),
            SkillGroups = skillGroups,
            Experiences = _mapper.Map<IEnumerable<ExperienceResponse>>(experiences),
            Services = _mapper.Map<IEnumerable<ServiceResponse>>(services.OrderBy(s => s.DisplayOrder)),
            Testimonials = _mapper.Map<IEnumerable<TestimonialResponse>>(testimonials),
            SocialLinks = _mapper.Map<IEnumerable<SocialLinkResponse>>(socialLinks)
        };
    }

    public async Task<ProfileResponse?> GetProfileAsync(CancellationToken cancellationToken = default)
    {
        var profile = await _profileRepository.GetPrimaryProfileAsync(cancellationToken);
        return _mapper.Map<ProfileResponse>(profile);
    }
}