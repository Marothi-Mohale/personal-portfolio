using MarothiMohale.Portfolio.Api.Api.Configuration;
using MarothiMohale.Portfolio.Api.Api.Middleware;
using MarothiMohale.Portfolio.Api.Application.Interfaces;
using MarothiMohale.Portfolio.Api.Application.Mapping;
using MarothiMohale.Portfolio.Api.Application.Services;
using MarothiMohale.Portfolio.Api.Domain.Entities;
using MarothiMohale.Portfolio.Api.Infrastructure.Data;
using MarothiMohale.Portfolio.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Register repositories
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ITestimonialRepository, TestimonialRepository>();
builder.Services.AddScoped<ISocialLinkRepository, SocialLinkRepository>();
builder.Services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
builder.Services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();

// Register services
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IAdminService, AdminService>();

// Configure options
builder.Services.Configure<PortfolioOptions>(builder.Configuration.GetSection(PortfolioOptions.SectionName));

// Add health checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Custom middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

// Health check endpoint
app.MapHealthChecks("/health");

await app.InitialiseDatabaseAsync();

app.Run();