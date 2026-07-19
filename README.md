# 🚀 TradeMaster - Enterprise Trading & Portfolio Management System

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=.net)
![C#](https://img.shields.io/badge/C%23-Backend-239120?style=for-the-badge&logo=c-sharp)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-red?style=for-the-badge&logo=microsoftsqlserver)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework-Core-6DB33F?style=for-the-badge)
![JWT](https://img.shields.io/badge/JWT-Authentication-orange?style=for-the-badge)
![GitHub Actions](https://img.shields.io/badge/GitHub%20Actions-CI/CD-2088FF?style=for-the-badge&logo=githubactions)

</div>

---

## 📖 Project Overview

TradeMaster is an enterprise-grade backend trading platform developed using **ASP.NET Core Web API** following **Clean Architecture** principles.

The application simulates how modern brokerage and investment platforms manage users, stocks, portfolios, holdings, watchlists, and trade execution through secure REST APIs.

The project demonstrates scalable backend development practices including Repository Pattern, Dependency Injection, JWT Authentication, Entity Framework Core, SQL Server integration, Unit Testing, Logging, and Continuous Integration using GitHub Actions.

The primary objective of this project is to build a production-style backend architecture that reflects real-world financial trading systems.

---

## ✨ Key Features

- 🔐 JWT Authentication & Authorization
- 🔒 BCrypt Password Hashing
- 👤 User Registration & Login
- 📈 Stock Management
- 💰 Buy & Sell Order Processing
- 📊 Portfolio Management
- 📦 Holdings Management
- ⭐ Watchlist Management
- 📜 Transaction History
- 🔍 Search, Sorting & Pagination
- 🧩 Repository Pattern
- 💉 Dependency Injection
- 🛡️ Global Exception Middleware
- 📝 Serilog File & Console Logging
- 🧪 Unit Testing using xUnit & Moq
- ⚙️ GitHub Actions CI Pipeline
- 🐳 Docker Support (Work in Progress)

---
# 🛠️ Tech Stack

| Category | Technologies |
|----------|--------------|
| **Language** | C# |
| **Framework** | ASP.NET Core 8 Web API |
| **Database** | SQL Server |
| **ORM** | Entity Framework Core |
| **Authentication** | JWT Bearer Authentication |
| **Password Security** | BCrypt |
| **Logging** | Serilog |
| **API Testing** | Swagger (OpenAPI) |
| **Testing** | xUnit, Moq |
| **CI/CD** | GitHub Actions |
| **Containerization** | Docker *(Docker Compose - In Progress)* |
| **Version Control** | Git & GitHub |

---

# 🏛️ Project Architecture

TradeMaster follows **Clean Architecture**, ensuring separation of concerns and making the application scalable, testable, and maintainable.

```
                Client / Swagger
                       │
                       ▼
              ASP.NET Core Web API
                       │
                Controllers Layer
                       │
                       ▼
                 Service Layer
          (Business Logic & Validation)
                       │
                       ▼
              Repository Layer
                       │
                       ▼
           Entity Framework Core
                       │
                       ▼
                  SQL Server
```

### Architecture Layers

### 📌 API Layer

Responsible for:

- Receiving HTTP requests
- Model Validation
- Authentication
- Returning HTTP responses

---

### 📌 Application Layer

Contains:

- Business Logic
- Service Interfaces
- DTOs
- Validation Rules

---

### 📌 Infrastructure Layer

Contains:

- Entity Framework Core
- Repository Implementations
- JWT Service
- Database Access
- Logging

---

### 📌 Core Layer

Contains:

- Entities
- Repository Interfaces
- Domain Models

---

# 📂 Project Structure

```
TradeMaster
│
├── TradeMaster.API
│   ├── Controllers
│   ├── Middleware
│   ├── Program.cs
│   ├── appsettings.json
│   └── Dockerfile
│
├── TradeMaster.Application
│   ├── DTOs
│   ├── Interfaces
│   └── Services
│
├── TradeMaster.Core
│   ├── Entities
│   └── Interfaces
│
├── TradeMaster.Infrastructure
│   ├── Data
│   ├── Repositories
│   ├── Services
│   └── Migrations
│
├── TradeMaster.Tests
│
├── .github
│   └── workflows
│       └── dotnet.yml
│
├── docker-compose.yml
├── README.md
└── TradeMaster.sln
```

---

# 🔄 Request Flow

```
Client
   │
   ▼
Controller
   │
   ▼
Service
   │
   ▼
Repository
   │
   ▼
Entity Framework Core
   │
   ▼
SQL Server
```

The Repository Pattern keeps the business logic independent of the database implementation, making the application easier to test and maintain.
