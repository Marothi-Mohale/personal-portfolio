# Marothi Mohale Portfolio API

A production-ready ASP.NET Core Web API for the Marothi Mohale portfolio platform.

## Features

- RESTful API with clean architecture
- EF Core with SQLite database
- JWT authentication for admin endpoints
- FluentValidation for input validation
- Swagger/OpenAPI documentation
- Structured logging with Serilog
- Health checks
- Docker containerization

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- FluentValidation
- AutoMapper
- Serilog
- Swagger

## API Endpoints

### Public Endpoints

- `GET /api/v1/home` - Get portfolio home data
- `GET /api/v1/projects` - List all projects
- `GET /api/v1/projects/{slug}` - Get project details
- `POST /api/v1/contact` - Submit contact message

### Admin Endpoints (JWT Required)

- `GET /api/v1/admin/contact` - Get contact messages
- `PUT /api/v1/admin/contact/{id}/review` - Mark message as reviewed

### System Endpoints

- `GET /health` - Health check
- `GET /swagger` - API documentation

## Local Development

### Prerequisites

- .NET 8 SDK
- Docker (optional)

### Running Locally

1. Navigate to the API directory:
   ```bash
   cd src/backend/MarothiMohale.Portfolio.Api
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:5001` and `http://localhost:5000`.

### Using Docker

1. Build and run with Docker Compose:
   ```bash
   docker compose up --build
   ```

The API will be available at `http://localhost:8080`.

## Configuration

The API uses `appsettings.json` for configuration. Key settings:

- `ConnectionStrings:DefaultConnection` - SQLite database path
- `Portfolio:PersistentDataRootPath` - Data persistence path
- `Serilog` - Logging configuration

## Database

The API uses SQLite with EF Core migrations. On startup, migrations are applied automatically and seed data is inserted.

To create a new migration:
```bash
dotnet ef migrations add <MigrationName> --project src/backend/MarothiMohale.Portfolio.Api
```

## Testing

Run tests:
```bash
dotnet test
```

## Architecture

The API follows Clean Architecture principles:

- **Domain**: Entities and business rules
- **Application**: Services and DTOs
- **Infrastructure**: Data access and external services
- **API**: Controllers and middleware

## Security

- Input validation with FluentValidation
- JWT authentication for admin endpoints
- CORS configuration for frontend
- Secure defaults and error handling