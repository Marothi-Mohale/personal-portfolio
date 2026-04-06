using AutoMapper;
using MarothiMohale.Portfolio.Api.Application.DTOs.Responses;
using MarothiMohale.Portfolio.Api.Domain.Entities;

namespace MarothiMohale.Portfolio.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Profile, ProfileResponse>();
        CreateMap<Project, ProjectSummaryResponse>();
        CreateMap<Project, ProjectDetailResponse>();
        CreateMap<Skill, SkillResponse>();
        CreateMap<Experience, ExperienceResponse>();
        CreateMap<Service, ServiceResponse>();
        CreateMap<Testimonial, TestimonialResponse>();
        CreateMap<SocialLink, SocialLinkResponse>();
        CreateMap<ContactMessage, ContactMessageResponse>();
    }
}