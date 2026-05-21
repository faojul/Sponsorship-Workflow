# Sponsorship Workflow Management System

A full-stack sponsorship workflow management system built with modern enterprise architecture principles using ASP.NET Core (.NET 10), Angular 20, PostgreSQL, JWT Authentication, CQRS, and Clean Architecture.

The system manages the full sponsorship approval lifecycle from request creation to finance approval with role-based workflow management.

---

# Live Demo

## Frontend
https://graceful-valkyrie-3fb4b1.netlify.app/login

## Backend Swagger
https://sponsorship-workflow.onrender.com/swagger

## GitHub Repository
https://github.com/faojul/Sponsorship-Workflow

> Note:
> The backend is hosted on Render free tier.
> On the very first visit, Render may take some time to spin up the server after inactivity.
> Once initialized, the application responds instantly.
> A loading indicator has been implemented in the frontend to handle backend cold starts gracefully.

---

# Project Overview

This application was designed as an enterprise-style workflow management system where different organizational roles collaborate through a structured sponsorship approval process.

The project demonstrates modern enterprise full-stack application development including:

* enterprise backend architecture
* scalable frontend structure
* secure authentication & authorization
* workflow-driven business logic
* modern Angular standalone architecture
* clean separation of concerns
* maintainable and testable code organization

---

# Tech Stack

## Backend

* ASP.NET Core (.NET 10)
* Clean Architecture
* CQRS + MediatR
* Entity Framework Core
* PostgreSQL
* ASP.NET Identity
* JWT Authentication
* FluentValidation
* Swagger / OpenAPI
* API Versioning

## Frontend

* Angular 20
* Angular Material
* Standalone Components
* Reactive Forms
* JWT Authentication
* Route Guards
* HTTP Interceptors
* Role-based Navigation
* Responsive Layout

## Deployment

* Frontend Hosting: Netlify (Free Tier)
* Backend Hosting: Render (Free Tier)
* Database Hosting: Neon PostgreSQL (Free Tier)
* Backend Deployment: Dockerized ASP.NET Core Application

---

# Core Features

## Authentication & Authorization

* JWT-based authentication
* Role-based authorization
* ASP.NET Identity integration
* Angular route protection
* Role-based UI rendering
* Swagger JWT authorization support

---

# Sponsorship Workflow Features

## Requestor Features

* Create sponsorship request draft
* Submit sponsorship request
* Cancel sponsorship request
* View personal requests
* View request status
* View workflow history

## Manager Features

* View pending manager approvals
* Approve sponsorship requests
* Reject sponsorship requests

## Finance Features

* View pending finance reviews
* Approve sponsorship requests
* Reject sponsorship requests

## System Admin Features

* View all sponsorship requests
* View workflow history of any request
* Manage sponsorship types
* Create sponsorship types
* Update sponsorship types
* Delete sponsorship types

---

# Workflow Lifecycle

```text
Draft
   ↓
Pending Manager Approval
   ↓
Pending Finance Review
   ↓
Approved / Rejected
```

---

# User Roles

| Role         | Responsibility                                 |
| ------------ | ---------------------------------------------- |
| Requestor    | Create and submit sponsorship requests         |
| Manager      | Approve or reject requests                     |
| FinanceAdmin | Final finance approval/rejection               |
| SystemAdmin  | Manage sponsorship types and monitor workflows |

---

# Backend Architecture

The backend follows Clean Architecture principles with clear separation of concerns.

```text
backend/
│
├── src/
│   ├── Sponsorship.Api
│   ├── Sponsorship.Application
│   ├── Sponsorship.Domain
│   └── Sponsorship.Infrastructure
│
└── tests/
```

---

# Layer Responsibilities

## Sponsorship.Api

Responsible for:

* Controllers
* Middleware
* API configuration
* Swagger configuration
* Authentication configuration
* Exception handling

## Sponsorship.Application

Responsible for:

* CQRS Commands & Queries
* MediatR Handlers
* Validators
* DTOs
* Business rules
* Workflow orchestration

## Sponsorship.Domain

Responsible for:

* Entities
* Enums
* Domain constants
* Core business models

## Sponsorship.Infrastructure

Responsible for:

* EF Core persistence
* PostgreSQL integration
* Identity implementation
* JWT token generation
* Database migrations
* Seed data

---

# Frontend Architecture

The frontend uses Angular 20 standalone component architecture with feature-based organization.

```text
frontend/
│
├── core/
│   ├── guards/
│   ├── interceptors/
│   ├── layouts/
│   └── services/
│
├── features/
│   ├── auth/
│   ├── dashboard/
│   ├── sponsorship-requests/
│   ├── sponsorship-types/
│   ├── manager-approvals/
│   ├── finance-approvals/
│   ├── workflow-history/
│   └── admin/
│
├── shared/
│
└── environments/
```

---

# Frontend Design Decisions

## Standalone Components

Angular standalone components were used to:

* reduce module complexity
* simplify lazy loading
* improve maintainability
* modernize Angular architecture

## Feature-Based Folder Structure

Frontend features were separated by business domain:

* authentication
* requests
* approvals
* admin management

This improves scalability and maintainability.

## Angular Material

Angular Material was used for:

* consistent UI design
* rapid development
* responsive layout
* accessible components

## HTTP Interceptor

A JWT interceptor automatically:

* attaches access tokens
* centralizes authentication handling
* simplifies API communication

## Route Guards

Protected routes ensure:

* authenticated access
* role-based navigation
* secure frontend routing

---

# Key Frontend Screens

## Authentication

* Login page
* Seed test accounts visible for easy reviewer testing

## Dashboard

* Quick navigation hub
* Role-based menu visibility
* Workflow access shortcuts

## Request Management

* My Requests page
* Create Request form
* Submit / Cancel actions
* Status tracking

## Approval Screens

* Manager Approval page
* Finance Approval page

## Workflow Tracking

* Workflow history page
* Full workflow transition audit trail

## Admin Screens

* All requests view
* Sponsorship type management
* Inline edit/update/delete support

---

# API Endpoints

## Authentication

| Method | Endpoint             | Description |
| ------ | -------------------- | ----------- |
| POST   | `/api/v1/Auth/login` | User login  |

---

## Sponsorship Requests

| Method | Endpoint                                           | Description             |
| ------ | -------------------------------------------------- | ----------------------- |
| POST   | `/api/v1/SponsorshipRequests/draft`                | Create draft            |
| POST   | `/api/v1/SponsorshipRequests/{id}/submit`          | Submit request          |
| POST   | `/api/v1/SponsorshipRequests/{id}/cancel`          | Cancel request          |
| POST   | `/api/v1/SponsorshipRequests/{id}/manager-approve` | Manager approval        |
| POST   | `/api/v1/SponsorshipRequests/{id}/manager-reject`  | Manager rejection       |
| POST   | `/api/v1/SponsorshipRequests/{id}/finance-approve` | Finance approval        |
| POST   | `/api/v1/SponsorshipRequests/{id}/finance-reject`  | Finance rejection       |
| GET    | `/api/v1/SponsorshipRequests/mine`                 | My requests             |
| GET    | `/api/v1/SponsorshipRequests`                      | All / filtered requests |
| GET    | `/api/v1/SponsorshipRequests/{id}/history`         | Workflow history        |

---

## Sponsorship Types

| Method | Endpoint                        | Description             |
| ------ | ------------------------------- | ----------------------- |
| GET    | `/api/v1/SponsorshipTypes`      | Get sponsorship types   |
| POST   | `/api/v1/SponsorshipTypes`      | Create sponsorship type |
| PUT    | `/api/v1/SponsorshipTypes/{id}` | Update sponsorship type |
| DELETE | `/api/v1/SponsorshipTypes/{id}` | Delete sponsorship type |

---

# Database Design

## Main Tables

* AspNetUsers
* AspNetRoles
* SponsorshipRequests
* SponsorshipTypes
* WorkflowHistories

---

# Seed Data

The application automatically seeds:

## Roles

* Requestor
* Manager
* FinanceAdmin
* SystemAdmin

## Test Users

| Email                                           | Password | Role         |
| ----------------------------------------------- | -------- | ------------ |
| [requestor@test.com](mailto:requestor@test.com) | Test123! | Requestor    |
| [manager@test.com](mailto:manager@test.com)     | Test123! | Manager      |
| [finance@test.com](mailto:finance@test.com)     | Test123! | FinanceAdmin |
| [admin@test.com](mailto:admin@test.com)         | Test123! | SystemAdmin  |

---
# Production Deployment Architecture

The application is deployed using a fully cloud-hosted architecture.

## Hosting Providers

| Service | Platform |
|---|---|
| Frontend | Netlify |
| Backend API | Render |
| Database | Neon PostgreSQL |

## Deployment Details

### Frontend
- Angular 20 production build deployed to Netlify
- SPA route handling configured
- Environment-based API configuration enabled

### Backend
- ASP.NET Core (.NET 10) API deployed on Render
- Dockerized deployment using multi-stage Docker build
- Environment variables configured securely
- Swagger enabled for production testing
- PostgreSQL cloud connection configured via environment variables

### Database
- PostgreSQL hosted on Neon cloud platform
- EF Core migrations applied automatically during deployment

## Production URLs
Frontend:
https://graceful-valkyrie-3fb4b1.netlify.app/login

Swagger API:
https://sponsorship-workflow.onrender.com/swagger


# Running the Backend Locally

## Prerequisites

* .NET 10 SDK
* PostgreSQL
* Visual Studio 2022 / Rider / VS Code

---

## Configure Connection String

Update:

```json
appsettings.json
```

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=SponsorshipDb;Username=postgres;Password=yourpassword"
}
```
---

## Apply Migrations

```bash
dotnet ef database update
```

---

## Run Backend

```bash
dotnet run
```

# Running via Docker

## Prerequisites

Docker Desktop

## Build Docker Image

```bash
docker build -t sponsorship-api .
```
Run Docker Container

```bash
docker run -p 8080:8080 sponsorship-api
```
## Docker Notes
- Multi-stage Docker build implemented
- Production-ready ASP.NET Core container setup
- Environment variable support enabled
- Suitable for cloud deployment platforms like Render

---

# Swagger Authentication

1. Login using seeded credentials
2. Copy JWT token
3. Click `Authorize` in Swagger
4. Enter:

```text
Bearer your_token_here
```

---

# Running the Frontend Locally

## Prerequisites

* Node.js 22+
* Angular CLI

---

## Install Dependencies

```bash
npm install
```

---

## Configure Environment

Update:

```ts
environment.ts
```

```ts
apiUrl: 'https://localhost:5001/api/v1'
```

---

## Run Angular Application

```bash
ng serve
```

Frontend will run at:

```text
http://localhost:4200
```

---

# Security Considerations

* JWT token authentication
* Role-based authorization
* Secure password hashing via ASP.NET Identity
* Protected API endpoints
* Angular route guards
* Centralized JWT interceptor
* Validation using FluentValidation

---

# Design Decisions

## Clean Architecture

Implemented to maintain:

* scalability
* maintainability
* testability
* separation of concerns

## CQRS + MediatR

Used to separate:

* Commands (write operations)
* Queries (read operations)

This improves workflow-oriented business logic handling and keeps controllers thin.

## Result Pattern

Implemented for:

* standardized API responses
* consistent error handling
* cleaner validation responses

## Entity Framework Core + PostgreSQL

Chosen for:

* strong ecosystem support
* maintainability
* rapid enterprise application development

## Angular Standalone Architecture

Chosen to:

* simplify frontend structure
* modernize Angular implementation
* reduce boilerplate
* improve lazy loading

---

# Challenges & Solutions

## Challenge

Managing workflow transitions cleanly across multiple roles.

## Solution

Implemented CQRS with dedicated command handlers for each workflow action.

---

## Challenge

Keeping controllers lightweight and maintainable.

## Solution

Used MediatR handlers with thin controllers and centralized application logic.

---

## Challenge

Managing role-based frontend navigation securely.

## Solution

Implemented Angular route guards and role-based menu rendering using JWT claims.

---

# Future Improvements

* Refresh token implementation
* CI/CD pipeline
* Unit & integration tests
* Email notifications
* Advanced analytics dashboard
* File attachment support
* Caching & performance optimization
* Audit logging
* Export reports

---

# Project Status

## Backend
Completed

## Frontend
Completed

## Workflow
Completed

## Deployment
Completed

## Cloud Hosting
Completed

## Dockerization
Completed

---

# Author

Faojul Ahsan
Senior Software Developer

Backend Focus:
ASP.NET Core | Clean Architecture | CQRS | PostgreSQL

Frontend:
Angular 20 | Angular Material | Standalone Components

Cloud & Deployment:
Docker | Render | Netlify | Neon PostgreSQL
