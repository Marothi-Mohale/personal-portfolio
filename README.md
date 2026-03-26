# Marothi Mohale Portfolio

Production-quality ASP.NET Core 8 portfolio application for Marothi Mohale with MVC, EF Core, SQLite, Identity, admin content management, Docker support, seeded portfolio content, and a polished recruiter-focused UI.

## Architecture Overview

This solution uses **ASP.NET Core MVC** rather than Razor Pages as the primary app pattern because the product naturally splits into:

- public marketing-style pages with controller-driven routing and SEO metadata
- an authenticated admin area with multiple CRUD workflows
- shared layout, view models, and route conventions that benefit from MVC separation

Identity UI still runs through Razor Pages for account management, which keeps authentication secure without over-scaffolding.

Project structure:

- `MarothiMohale.Portfolio.Web/Data` for `ApplicationDbContext`, startup seeding, and EF Core migrations
- `MarothiMohale.Portfolio.Web/Models` for the domain entities and `ApplicationUser`
- `MarothiMohale.Portfolio.Web/Services` for portfolio queries, contact handling, dashboard data, and health checks
- `MarothiMohale.Portfolio.Web/ViewModels` for public-page and dashboard shaping
- `MarothiMohale.Portfolio.Web/Controllers` for public routes and error handling
- `MarothiMohale.Portfolio.Web/Areas/Admin` for authenticated management workflows
- `MarothiMohale.Portfolio.Web/Middleware` for global exception handling
- `MarothiMohale.Portfolio.Web/wwwroot` for CSS, JavaScript, and seeded project artwork

## Features

- Premium responsive landing page with hero, skills, services, featured projects, testimonials, experience timeline, and insights
- Projects listing and project details pages backed by SQLite data
- Contact form with server validation, antiforgery protection, and persistent message storage
- Blog/insights structure ready for future editorial content
- Admin area secured with ASP.NET Core Identity and role-based authorization
- Seeded profile, skills, experiences, services, testimonials, social links, and six requested portfolio projects
- Global exception middleware, custom 404/500 pages, structured logging defaults, and `/health` endpoint
- Automatic database migration and seed execution on startup

## Local Run

Requirements:

- .NET SDK 8.0+

Commands:

```powershell
dotnet restore .\MarothiMohale.Portfolio.Web\MarothiMohale.Portfolio.Web.csproj --configfile .\NuGet.Config
dotnet build .\MarothiMohale.Portfolio.sln
dotnet run --project .\MarothiMohale.Portfolio.Web\MarothiMohale.Portfolio.Web.csproj
```

App URLs:

- `https://localhost:7235`
- `http://localhost:5128`

Seeded admin login:

- Email: `admin@marothimohale.dev`
- Password: `MarothiAdmin1`

Admin sign-in URL:

- `/Identity/Account/Login`

How to change the seeded admin credentials:

1. Update the `AdminUser` section in `MarothiMohale.Portfolio.Web/appsettings.json`, or override with environment variables:
   - `AdminUser__Email`
   - `AdminUser__Password`
2. Do this **before first run** if you want the first seeded account to use different credentials.
3. If the admin user was already created, change the password through Identity or:
   - update the config
   - delete the existing SQLite database
   - start the app again so the seeded admin user is recreated

## Docker

Build and run everything with one command:

```powershell
docker compose up --build
```

The app is exposed on:

- `http://localhost:8080`

SQLite persistence is handled by the named Docker volume `portfolio-data`.

## Migrations

Create a new migration:

```powershell
dotnet tool run dotnet-ef migrations add <MigrationName> --project .\MarothiMohale.Portfolio.Web\MarothiMohale.Portfolio.Web.csproj --startup-project .\MarothiMohale.Portfolio.Web\MarothiMohale.Portfolio.Web.csproj --output-dir Data\Migrations
```

Apply migrations manually if needed:

```powershell
dotnet tool run dotnet-ef database update --project .\MarothiMohale.Portfolio.Web\MarothiMohale.Portfolio.Web.csproj --startup-project .\MarothiMohale.Portfolio.Web\MarothiMohale.Portfolio.Web.csproj
```

In normal operation, the app applies migrations and seed data automatically on startup.

## Operational Notes

- Contact messages are stored in SQLite for later review.
- The admin area is restricted to the seeded `Administrator` role.
- Admin management currently includes dashboard summary, Projects CRUD, Skills CRUD, Services CRUD, Testimonials CRUD, Profile editing, and contact message review.
- The health endpoint is available at `/health`.
- The theme toggle stores the user preference in local storage.

## Assumptions

- The requested six repositories to seed were the explicit list in the brief: `api design`, `levels_on_ice`, `nickys-manicure`, `secureHelpdesk`, `clinic-admin`, and `Perfect Tutorials`.
- Because a specific GitHub profile URL was not provided, repository URLs were inferred using the `Marothi-Mohale` GitHub username format.
- Testimonials were intentionally seeded as placeholders so the content model and admin workflow are ready without inventing fake client endorsements.
