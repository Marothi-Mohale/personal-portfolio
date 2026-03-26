using MarothiMohale.Portfolio.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarothiMohale.Portfolio.Web.Data;

public static class ApplicationDbInitializer
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        var configuration = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseInitialiser");
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        await context.Database.MigrateAsync();
        await SeedRolesAsync(roleManager);
        await SeedAdminAsync(configuration, userManager);
        await SeedPortfolioAsync(context, logger);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        const string adminRole = "Administrator";
        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }
    }

    private static async Task SeedAdminAsync(IConfiguration configuration, UserManager<ApplicationUser> userManager)
    {
        var adminEmail = configuration["AdminUser:Email"] ?? "admin@marothimohale.dev";
        var adminPassword = configuration["AdminUser:Password"] ?? "MarothiAdmin1";

        var user = await userManager.FindByEmailAsync(adminEmail);
        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                DisplayName = "Marothi Mohale"
            };

            var result = await userManager.CreateAsync(user, adminPassword);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unable to create seeded admin user: {string.Join(", ", result.Errors.Select(x => x.Description))}");
            }
        }

        if (!await userManager.IsInRoleAsync(user, "Administrator"))
        {
            await userManager.AddToRoleAsync(user, "Administrator");
        }
    }

    private static async Task SeedPortfolioAsync(ApplicationDbContext context, ILogger logger)
    {
        var profile = await context.Profiles.OrderBy(x => x.Id).FirstOrDefaultAsync();
        if (profile is null)
        {
            profile = new Profile();
            context.Profiles.Add(profile);
        }

        profile.FullName = "Marothi Mohale";
        profile.Headline = "Software Developer, C# / ASP.NET Core builder, full-stack web developer, and data engineering enthusiast.";
        profile.ProfessionalSummary = "I build dependable .NET and web solutions with strong product thinking, practical UI craftsmanship, and a growing passion for data engineering and analytics-driven systems.";
        profile.About = "I am Marothi Mohale, a software developer with a BSc in Computer Science and Business Computing. My work sits at the intersection of C# / ASP.NET Core delivery, full-stack web development, and practical data engineering curiosity. I care about software that feels professional, reads clearly, and solves real business problems with reliability and polish.";
        profile.Email = "mohalemarothi@gmail.com";
        profile.Phone = "072 069 7425";
        profile.Location = "Cape Town, South Africa";
        profile.ResumeUrl = "#resume";
        profile.HeroPrimaryCtaUrl = "/Projects";
        profile.HeroSecondaryCtaUrl = "/Contact";

        if (!await context.Projects.AnyAsync())
        {
            var now = DateTime.UtcNow;
            context.Projects.AddRange(
                SeedProject("api design", "api-design", "REST API design work focused on clean resource modelling, validation, and developer-friendly contracts.", "A backend-focused project that showcases thoughtful endpoint design, maintainable contract structures, and a practical approach to building APIs that are easy to consume, test, and evolve.", "C#, ASP.NET Core, REST, Validation, JSON", "https://github.com/Marothi-Mohale/api-design", "/images/project-api-design.svg", true, 1, now.AddMonths(-8), now.AddMonths(-2)),
                SeedProject("levels_on_ice", "levels-on-ice", "Interactive application work with strong UI thinking, user flow awareness, and implementation polish.", "This project highlights front-end sensitivity paired with software engineering discipline. It demonstrates an ability to turn an idea into an experience with structured screens, clear interactions, and maintainable code organization.", "C#, UI Engineering, Responsive Layouts, Product UX", "https://github.com/Marothi-Mohale/levels_on_ice", "/images/project-levels-on-ice.svg", true, 2, now.AddMonths(-7), now.AddMonths(-3)),
                SeedProject("nickys-manicure", "nickys-manicure", "Business-facing web solution shaped around service presentation, usability, and conversion-friendly structure.", "A client-oriented project that centers on practical web delivery: showcasing services, improving customer clarity, and building a clean, approachable digital experience.", "HTML, CSS, JavaScript, Web Design, UX", "https://github.com/Marothi-Mohale/nickys-manicure", "/images/project-nickys-manicure.svg", true, 3, now.AddMonths(-6), now.AddMonths(-2)),
                SeedProject("secureHelpdesk", "secure-helpdesk", "Secure workflow-oriented support platform work with an emphasis on permissions, process, and reliability.", "This project reflects an operational systems mindset: handling support workflows, protecting data flow, and designing features that prioritize trust, control, and maintainability.", "C#, ASP.NET Core, Security, Identity, CRUD", "https://github.com/Marothi-Mohale/secureHelpdesk", "/images/project-secure-helpdesk.svg", false, 4, now.AddMonths(-5), now.AddMonths(-1)),
                SeedProject("clinic-admin", "clinic-admin", "Administrative system design for operational visibility, structured records, and day-to-day clinic workflows.", "An administration-focused application built around practical management needs. The project demonstrates data structures, role-aware workflows, and software that supports staff productivity.", "C#, .NET, Admin Workflows, Data Modelling", "https://github.com/Marothi-Mohale/clinic-admin", "/images/project-clinic-admin.svg", false, 5, now.AddMonths(-4), now.AddMonths(-1)),
                SeedProject("Perfect Tutorials", "perfect-tutorials", "Education-oriented platform work that blends content organisation, clarity, and approachable technical communication.", "A tutorial-focused product that demonstrates the ability to structure information clearly and present technical or educational material in an accessible way.", "C#, Content Delivery, Education UX, Web Development", "https://github.com/Marothi-Mohale/Perfect-Tutorials", "/images/project-perfect-tutorials.svg", false, 6, now.AddMonths(-3), now.AddMonths(-1)));
        }

        if (!await context.Skills.AnyAsync())
        {
            context.Skills.AddRange(
                SeedSkill("ASP.NET Core", "C# / ASP.NET Core", 92, "Building maintainable MVC and API applications with strong conventions and clean composition.", 1),
                SeedSkill("Entity Framework Core", "C# / ASP.NET Core", 88, "Designing practical code-first data access with sensible boundaries and migrations.", 2),
                SeedSkill("Razor & MVC", "Web Development", 86, "Delivering server-rendered experiences with strong accessibility and SEO foundations.", 3),
                SeedSkill("HTML, CSS, JavaScript", "Web Development", 84, "Crafting responsive interfaces with polished interaction patterns and clean implementation.", 4),
                SeedSkill("SQLite & SQL", "Databases", 82, "Designing relational structures, queries, and persistent content flows for real apps.", 5),
                SeedSkill("Data Modelling", "Databases", 80, "Structuring entities around business needs while preserving future extensibility.", 6),
                SeedSkill("ETL Thinking", "Data Engineering", 76, "Breaking down data ingestion and transformation workflows into reliable steps.", 7),
                SeedSkill("Analytics Pipelines", "Data Engineering", 72, "Connecting application data to operational reporting and downstream analysis needs.", 8),
                SeedSkill("Docker", "Cloud / DevOps", 78, "Containerising applications for predictable local and deployment-ready environments.", 9),
                SeedSkill("CI/CD Mindset", "Cloud / DevOps", 70, "Designing codebases with predictable build, test, and deployment workflows in mind.", 10));
        }

        if (!await context.Experiences.AnyAsync())
        {
            context.Experiences.AddRange(
                new Experience { Title = "Independent Software Developer", Organization = "Personal and client-focused projects", Location = "South Africa", Summary = "Building web applications, data-aware business tools, and portfolio-grade software with an emphasis on clean architecture, usability, and reliability.", StartDate = new DateTime(2023, 1, 1), IsCurrent = true, DisplayOrder = 1 },
                new Experience { Title = "Technical Tutor and Builder", Organization = "Tutorial and learning projects", Location = "Remote", Summary = "Creating educational material and learning-oriented software that makes technical concepts accessible while reinforcing practical implementation skills.", StartDate = new DateTime(2022, 1, 1), EndDate = new DateTime(2023, 12, 31), DisplayOrder = 2 });
        }

        if (!await context.Testimonials.AnyAsync())
        {
            context.Testimonials.AddRange(
                new Testimonial { AuthorName = "Future Client Placeholder", AuthorRole = "Client feedback slot", Company = "Portfolio seed content", Quote = "This section is scaffolded so genuine client or collaborator feedback can be added later through the admin area without code changes.", IsFeatured = true, DisplayOrder = 1 },
                new Testimonial { AuthorName = "Future Colleague Placeholder", AuthorRole = "Peer recommendation slot", Company = "Portfolio seed content", Quote = "The testimonial model, admin workflow, and UI are production-ready even though the initial content is intentionally placeholder text.", IsFeatured = true, DisplayOrder = 2 });
        }

        if (!await context.Services.AnyAsync())
        {
            context.Services.AddRange(
                new ServiceOffering { Title = "Web Development", Description = "Responsive business websites and custom web applications built with modern .NET and sound UI fundamentals.", DisplayOrder = 1 },
                new ServiceOffering { Title = "API Development", Description = "Robust backend APIs with clean contracts, validation, and maintainable service design.", DisplayOrder = 2 },
                new ServiceOffering { Title = "Data Engineering Solutions", Description = "Application-adjacent data modelling, transformation thinking, and reporting-focused system design.", DisplayOrder = 3 },
                new ServiceOffering { Title = "Tutoring / Technical Training", Description = "Technical mentoring, coaching, and guided learning support for software development concepts and projects.", DisplayOrder = 4 });
        }

        var socialLinks = await context.SocialLinks.OrderBy(x => x.DisplayOrder).ToListAsync();
        while (socialLinks.Count < 3)
        {
            var socialLink = new SocialLink();
            socialLinks.Add(socialLink);
            context.SocialLinks.Add(socialLink);
        }

        socialLinks[0].Platform = "GitHub";
        socialLinks[0].Url = "https://github.com/Marothi-Mohale";
        socialLinks[0].DisplayOrder = 1;

        socialLinks[1].Platform = "Email";
        socialLinks[1].Url = "mailto:mohalemarothi@gmail.com";
        socialLinks[1].DisplayOrder = 2;

        socialLinks[2].Platform = "UCT Email";
        socialLinks[2].Url = "mailto:mhlmar030@myuct.ac.za";
        socialLinks[2].DisplayOrder = 3;

        if (!await context.BlogPosts.AnyAsync())
        {
            context.BlogPosts.AddRange(
                new BlogPost { Title = "What I Optimise For In Business Software", Slug = "what-i-optimise-for-in-business-software", Excerpt = "A short look at the trade-offs behind clean architecture, maintainability, and delivery speed in practical .NET projects.", Content = "Strong business software balances clarity, resilience, and speed of iteration. I optimize for codebases that are easy to understand, easy to extend, and realistic to support over time.", PublishedAtUtc = DateTime.UtcNow.AddDays(-21) },
                new BlogPost { Title = "Why Portfolio Projects Should Feel Product-Ready", Slug = "why-portfolio-projects-should-feel-product-ready", Excerpt = "A portfolio should show not just code, but judgment: how you handle workflows, messaging, resilience, and presentation.", Content = "Recruiters and clients assess how you think. Product-ready portfolio projects show how you handle navigation, edge cases, error states, content management, and delivery.", PublishedAtUtc = DateTime.UtcNow.AddDays(-8) });
        }

        await context.SaveChangesAsync();
        logger.LogInformation("Portfolio seed data ensured");
    }

    private static Project SeedProject(string title, string slug, string shortDescription, string fullDescription, string techStack, string gitHubUrl, string imageUrl, bool isFeatured, int displayOrder, DateTime createdAt, DateTime updatedAt)
        => new() { Title = title, Slug = slug, ShortDescription = shortDescription, FullDescription = fullDescription, TechStack = techStack, GitHubUrl = gitHubUrl, ImageUrl = imageUrl, IsFeatured = isFeatured, DisplayOrder = displayOrder, CreatedAt = createdAt, UpdatedAt = updatedAt };

    private static Skill SeedSkill(string name, string category, int proficiency, string summary, int order)
        => new() { Name = name, Category = category, ProficiencyPercent = proficiency, Summary = summary, DisplayOrder = order };
}
