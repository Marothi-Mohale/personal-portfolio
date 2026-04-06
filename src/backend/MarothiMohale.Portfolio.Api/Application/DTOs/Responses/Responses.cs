namespace MarothiMohale.Portfolio.Api.Application.DTOs.Responses;

public class ProfileResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Headline { get; set; } = string.Empty;
    public string ProfessionalSummary { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? ResumeUrl { get; set; }
    public string? HeroPrimaryCtaUrl { get; set; }
    public string? HeroSecondaryCtaUrl { get; set; }
}

public class ProjectSummaryResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string TechStack { get; set; } = string.Empty;
    public string? GitHubUrl { get; set; }
    public string? LiveUrl { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProjectDetailResponse : ProjectSummaryResponse
{
    public string FullDescription { get; set; } = string.Empty;
}

public class SkillResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int ProficiencyPercent { get; set; }
    public string Summary { get; set; } = string.Empty;
}

public class SkillGroupResponse
{
    public string Category { get; set; } = string.Empty;
    public IEnumerable<SkillResponse> Skills { get; set; } = new List<SkillResponse>();
}

public class ExperienceResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string Summary { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent { get; set; }
}

public class ServiceResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class TestimonialResponse
{
    public int Id { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorRole { get; set; } = string.Empty;
    public string? Company { get; set; }
    public string Quote { get; set; } = string.Empty;
}

public class SocialLinkResponse
{
    public int Id { get; set; }
    public string Platform { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public class HomeResponse
{
    public ProfileResponse? Profile { get; set; }
    public IEnumerable<ProjectSummaryResponse> FeaturedProjects { get; set; } = new List<ProjectSummaryResponse>();
    public IEnumerable<SkillGroupResponse> SkillGroups { get; set; } = new List<SkillGroupResponse>();
    public IEnumerable<ExperienceResponse> Experiences { get; set; } = new List<ExperienceResponse>();
    public IEnumerable<ServiceResponse> Services { get; set; } = new List<ServiceResponse>();
    public IEnumerable<TestimonialResponse> Testimonials { get; set; } = new List<TestimonialResponse>();
    public IEnumerable<SocialLinkResponse> SocialLinks { get; set; } = new List<SocialLinkResponse>();
}

public class ContactMessageResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime SubmittedAtUtc { get; set; }
    public bool IsReviewed { get; set; }
}