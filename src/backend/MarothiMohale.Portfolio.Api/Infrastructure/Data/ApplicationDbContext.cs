using MarothiMohale.Portfolio.Api.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarothiMohale.Portfolio.Api.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<Testimonial> Testimonials => Set<Testimonial>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<SocialLink> SocialLinks => Set<SocialLink>();
    public DbSet<SiteSetting> SiteSettings => Set<SiteSetting>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Profile>(entity =>
        {
            entity.Property(x => x.FullName).HasMaxLength(150);
            entity.Property(x => x.Headline).HasMaxLength(200);
            entity.Property(x => x.Email).HasMaxLength(150);
            entity.Property(x => x.Location).HasMaxLength(120);
            entity.Property(x => x.ResumeUrl).HasMaxLength(255);
        });

        builder.Entity<Project>(entity =>
        {
            entity.HasIndex(x => x.Slug).IsUnique();
            entity.Property(x => x.Title).HasMaxLength(150);
            entity.Property(x => x.Slug).HasMaxLength(160);
            entity.Property(x => x.ShortDescription).HasMaxLength(320);
            entity.Property(x => x.GitHubUrl).HasMaxLength(255);
            entity.Property(x => x.LiveUrl).HasMaxLength(255);
            entity.Property(x => x.ImageUrl).HasMaxLength(255);
            entity.Property(x => x.TechStack).HasMaxLength(300);
        });

        builder.Entity<Skill>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(120);
            entity.Property(x => x.Category).HasMaxLength(80);
        });

        builder.Entity<Experience>(entity =>
        {
            entity.Property(x => x.Title).HasMaxLength(150);
            entity.Property(x => x.Organization).HasMaxLength(150);
            entity.Property(x => x.Location).HasMaxLength(120);
        });

        builder.Entity<Testimonial>(entity =>
        {
            entity.Property(x => x.AuthorName).HasMaxLength(120);
            entity.Property(x => x.AuthorRole).HasMaxLength(160);
            entity.Property(x => x.Company).HasMaxLength(160);
        });

        builder.Entity<ContactMessage>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(120);
            entity.Property(x => x.Email).HasMaxLength(150);
            entity.Property(x => x.Subject).HasMaxLength(160);
            entity.Property(x => x.Phone).HasMaxLength(40);
        });

        builder.Entity<Service>(entity =>
        {
            entity.Property(x => x.Title).HasMaxLength(140);
        });

        builder.Entity<SocialLink>(entity =>
        {
            entity.Property(x => x.Platform).HasMaxLength(50);
            entity.Property(x => x.Url).HasMaxLength(255);
        });

        builder.Entity<SiteSetting>(entity =>
        {
            entity.HasIndex(x => x.Key).IsUnique();
            entity.Property(x => x.Key).HasMaxLength(100);
            entity.Property(x => x.Description).HasMaxLength(200);
        });
    }
}