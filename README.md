# Marothi Mohale Portfolio

A production-oriented personal portfolio web application built with ASP.NET Core 8, EF Core, SQLite, and Docker. The project presents Marothi Mohale as a software developer with strong C# / ASP.NET Core, full-stack web, and emerging data engineering capability, while also demonstrating secure admin tooling, resilient startup behavior, and maintainable architecture.

## Project Overview

This application combines a polished public-facing portfolio with a protected admin area for managing content such as projects, skills, services, profile details, testimonials, and contact messages.

It is designed to be:

- recruiter-friendly in presentation
- practical to run locally or in Docker
- easy to extend with new content and features
- strong on fundamentals like validation, authentication, error handling, and operational safety

## Tech Stack

- C#
- ASP.NET Core 8 MVC
- ASP.NET Core Identity
- Entity Framework Core 8
- SQLite
- Razor views
- Custom CSS and lightweight JavaScript
- Docker and Docker Compose

## Architecture Overview

The solution uses ASP.NET Core MVC as the primary application pattern, with Identity UI handling authentication flows through Razor Pages.

High-level structure:

- public site controllers and views for the portfolio experience
- admin area for protected content management workflows
- EF Core for data access and code-first migrations
- focused service layer for portfolio queries, contact handling, dashboard summaries, and health checks
- middleware for exception handling and request logging
- startup initializer for safe migration and idempotent seed execution

This keeps the codebase clean and practical without introducing unnecessary abstraction.

## Why ASP.NET Core, SQLite, and Docker

### ASP.NET Core

ASP.NET Core was chosen because it provides excellent performance, strong built-in security features, mature dependency injection, first-class Identity support, and a clean path for both server-rendered UI and admin workflows.

### SQLite

SQLite is a strong fit for this project because the application manages structured content with a relatively simple deployment model. It keeps local development friction low, works well with EF Core migrations, and is easy to persist safely in a Docker volume.

### Docker

Docker makes the application easy to run consistently across machines. The project includes a multi-stage Dockerfile, Docker Compose support, volume-backed SQLite persistence, and environment-variable configuration so the app can be started with a predictable one-command workflow.

## Features

- premium responsive portfolio UI
- featured and full projects sections
- project detail pages
- about, skills, services, testimonials, experience, and insights sections
- contact form with validation and persistence
- admin area with role-based protection
- Projects, Skills, Services, Testimonials, and Profile management
- contact message review in the admin area
- global exception handling and custom error pages
- health check endpoint
- automatic migration and seed on startup

## Setup

### Prerequisites

- .NET SDK 8.0+
- Docker Desktop or compatible Docker runtime for container usage

### Clone and Restore

```powershell
git clone <repository-url>
cd marothi-mohale
dotnet restore .\MarothiMohale.Portfolio.Web\MarothiMohale.Portfolio.Web.csproj --configfile .\NuGet.Config
```

## Local Development

Build the solution:

```powershell
dotnet build .\MarothiMohale.Portfolio.sln
```

Run the web application:

```powershell
dotnet run --project .\MarothiMohale.Portfolio.Web\MarothiMohale.Portfolio.Web.csproj
```

Typical local URLs:

- `https://localhost:7235`
- `http://localhost:5128`

On startup, the application will:

- apply EF Core migrations
- create the SQLite database if needed
- seed profile, projects, skills, services, testimonials, blog data, and admin access

## EF Core Migrations

Create a new migration:

```powershell
dotnet tool run dotnet-ef migrations add <MigrationName> --project .\MarothiMohale.Portfolio.Web\MarothiMohale.Portfolio.Web.csproj --startup-project .\MarothiMohale.Portfolio.Web\MarothiMohale.Portfolio.Web.csproj --output-dir Data\Migrations
```

Apply migrations manually:

```powershell
dotnet tool run dotnet-ef database update --project .\MarothiMohale.Portfolio.Web\MarothiMohale.Portfolio.Web.csproj --startup-project .\MarothiMohale.Portfolio.Web\MarothiMohale.Portfolio.Web.csproj
```

In normal operation, manual migration is usually unnecessary because startup migration is built into the app.

## Docker

Build and run with Docker Compose:

```powershell
docker compose up --build
```

The application is exposed on:

- `http://localhost:8080`

### Container Notes

- the app runs in `Production`
- SQLite is stored in a mounted Docker volume
- data-protection keys are also persisted so admin auth remains stable across restarts
- migrations and seed data run automatically on first startup

Useful Docker commands:

```powershell
docker compose up --build
docker compose down
docker compose down -v
docker compose logs -f portfolio
```

Persistence behavior:

- `docker compose down` keeps the database volume
- `docker compose down -v` removes the volume and resets the database

## Default Admin Access

Seeded administrator account:

- Email: `admin@marothimohale.dev`
- Password: `MarothiAdmin1`

Admin login:

- `/Identity/Account/Login`

To change the seeded credentials before first run, update the `AdminUser` section in `MarothiMohale.Portfolio.Web/appsettings.json` or override with environment variables:

- `AdminUser__Email`
- `AdminUser__Password`

If the admin user already exists, either change the password through Identity or recreate the database and start the app again.

## Project Structure

```text
MarothiMohale.Portfolio.Web
├── Areas/Admin              # Protected admin controllers and views
├── Configuration            # Options and app configuration models
├── Controllers              # Public site controllers
├── Data                     # DbContext, seed initializer, migrations
├── Middleware               # Exception handling and request logging
├── Models                   # Domain entities and Identity user
├── Services                 # Business logic and query services
├── ViewModels               # UI-specific shaping models
├── Views                    # Public MVC views and shared layout
└── wwwroot                  # Static assets, styles, scripts, images
```

## Operational Highlights

- role-based admin authorization
- antiforgery protection on form posts
- server-side validation
- request logging
- graceful handling for missing content, images, and links
- configuration-driven startup behavior
- health endpoint at `/health`

## Future Improvements

- richer blog publishing and editorial workflows
- image upload and asset management from the admin area
- automated tests for controllers, services, and admin flows
- CI/CD pipeline for build, test, and container publish
- optional PostgreSQL support for larger deployments
- richer analytics or recruiter engagement tracking

## Summary

This project is intentionally built to feel like more than a simple portfolio. It demonstrates product thinking, secure admin workflows, clean ASP.NET Core architecture, operational readiness, and a polished developer-facing presentation in one maintainable codebase.
