# TodoApi

A REST API for task management built with ASP.NET Core 8, Entity Framework Core, and SQLite.

## Overview

This project provides create, read, partial update, and delete operations for tasks, with a focus on:

- clear separation of concerns (Controller, Service, Data);
- service contract through `ITaskService`;
- database persistence with EF Core;
- request validation using Data Annotations;
- automated tests for service and controller behavior.

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- xUnit
- Moq

## Project Structure

- `Controllers/`: HTTP endpoints.
- `Models/`: entities and DTOs.
- `Services/`: business logic and abstractions.
- `Data/`: EF Core context and design-time factory.
- `Migrations/`: database migration history.
- `TodoApi.Tests/`: automated tests.

## Service Architecture

The application contract is defined in `ITaskService`.

Available implementations:

- `TaskService`: in-memory implementation (useful for learning and comparison).
- `EfTaskService`: EF Core + SQLite implementation (current runtime implementation).

In `Program.cs`, dependency injection is configured as:

```csharp
builder.Services.AddScoped<ITaskService, EfTaskService>();
```

## Running the Project

Prerequisite:

- .NET 8 SDK installed

Steps:

1. Restore dependencies:

```bash
dotnet restore
```

2. Apply migrations:

```bash
dotnet ef database update
```

3. Run the API:

```bash
dotnet run
```

4. Open Swagger using the URL shown in the terminal (for example: `https://localhost:xxxx/swagger`).

## Endpoints

Base route: `api/tasks`

### POST `/api/tasks`

Creates a task.

Request body example:

```json
{
  "title": "Study ASP.NET Core",
  "description": "Review controller, service, and EF Core"
}
```

### GET `/api/tasks`

Returns all tasks.

### GET `/api/tasks/{id}`

Returns one task by ID.

If not found, returns `404` with a standardized payload:

```json
{
  "code": "TASK_NOT_FOUND",
  "message": "Tarefa nao encontrada.",
  "id": 10
}
```

### PATCH `/api/tasks/{id}`

Partially updates a task.

Request body example:

```json
{
  "title": "Study tests",
  "status": "EmAndamento"
}
```

### DELETE `/api/tasks/{id}`

Deletes a task by ID.

## Validation Rules

`CreateTaskDto`:

- `Title`: required, 3 to 100 characters.
- `Description`: required, 3 to 500 characters.

`UpdateTaskDto`:

- `Title` (optional): if provided, 3 to 100 characters.
- `Description` (optional): if provided, 3 to 500 characters.
- `Status` (optional): must be a valid enum value.
- At least one field must be sent in PATCH requests.

## Automated Tests

Test project: `TodoApi.Tests`

Run all tests:

```bash
dotnet test
```

Run only EF service tests:

```bash
dotnet test --filter EfTaskServiceTests
```

Run only controller tests:

```bash
dotnet test --filter TasksControllerTests
```

## Migrations

Create a new migration:

```bash
dotnet ef migrations add MigrationName
```

Apply migrations:

```bash
dotnet ef database update
```

List migrations:

```bash
dotnet ef migrations list
```
