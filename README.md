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


# 🗄️ Database Schema

TradeMaster uses **SQL Server** as the relational database and **Entity Framework Core (Code First)** for database modeling and migrations.

## Database Entities

| Entity | Description |
|---------|-------------|
| **Users** | Stores user profile and authentication information |
| **Stocks** | Stores available stocks for trading |
| **Orders** | Records buy and sell orders placed by users |
| **Holdings** | Maintains the current portfolio of each user |
| **Transactions** | Stores completed trade history |
| **Watchlists** | Stores stocks bookmarked by users |

---

Passwords are securely hashed using **BCrypt** before being stored in the database.

---

# 🔍 Search, Sorting & Pagination

TradeMaster supports efficient querying for large datasets.

### Pagination

```
GET /api/Stock?pageNumber=1&pageSize=10
```

### Search

```
GET /api/Stock?search=TCS
```

---

### Sorting

```
GET /api/Stock?sortBy=price&sortOrder=desc
```

---

### Combined Query

```
GET /api/Stock?pageNumber=1&pageSize=10&search=Reliance&sortBy=currentPrice&sortOrder=asc
```


# 📄 Sample API Response

```json
{
  "id": 1,
  "symbol": "TCS",
  "companyName": "Tata Consultancy Services",
  "currentPrice": 3725.50
}
```

# 🚀 Getting Started

## Prerequisites

Before running the project, ensure you have the following installed:

- .NET 8 SDK
- SQL Server
- Visual Studio 2022 / Visual Studio Code
- Git
- Docker Desktop *(Optional)*

---

## Clone the Repository

```bash
git clone https://github.com/<your-username>/TradeMaster.git
cd TradeMaster
```

---

## Configure the Database

Update the connection string in `appsettings.json`.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=TradeMaster;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

If using SQL Server Authentication:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=TradeMaster;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
}
```

---

## Apply Entity Framework Migrations

```bash
dotnet ef database update
```

---

## Run the Application

```bash
dotnet run
```

The API will be available at:

```
https://localhost:5001
```

or

```
http://localhost:5000
```

depending on your launch profile.

---

# 📖 Swagger API Documentation

Once the application is running, open:

```
https://localhost:5001/swagger
```

Swagger provides interactive documentation for all REST endpoints, allowing developers to test APIs directly from the browser.

---

# 🧪 Unit Testing

TradeMaster includes automated unit tests using **xUnit** and **Moq**.

Run all tests:

```bash
dotnet test
```

The test suite validates:

- Repository methods
- Service layer business logic
- Authentication functionality
- Trade execution workflows
- API behavior

---

# ⚙️ Continuous Integration

This project uses **GitHub Actions** to automate the build and test process.

The CI workflow performs the following tasks on every push and pull request:

- Restore NuGet packages
- Build the solution
- Execute unit tests
- Verify project integrity

---

# 🐳 Docker Support

Docker support has been added to simplify deployment using containers.

Current setup includes:

- Multi-stage Dockerfile
- Docker Compose
- SQL Server Container
- ASP.NET Core Container

> **Note:** Docker deployment is currently under active development and may require additional configuration depending on the local environment.

---

# 📸 Project Screenshots

The following screenshots can be added here:

- Login API
- Register API
- Swagger Home Page
- JWT Authentication
- Stock APIs
- Buy/Sell APIs
- Portfolio APIs
- SQL Server Database
- GitHub Actions Workflow

Example:

```markdown
![Swagger](images/swagger-home.png)
```

---

# 🚀 Future Enhancements

Some planned improvements include:

- Real-time Market Data Integration
- WebSocket-based Live Price Streaming
- Redis Caching
- Email Notifications
- Role-Based Access Control (RBAC)
- Refresh Token Authentication
- Audit Logging
- Portfolio Analytics Dashboard
- Performance Optimization
- Kubernetes Deployment

---

# 💡 Key Learning Outcomes

This project demonstrates practical experience with:

- ASP.NET Core Web API
- Clean Architecture
- SOLID Principles
- Repository Pattern
- Dependency Injection
- Entity Framework Core
- SQL Server
- JWT Authentication
- BCrypt Password Hashing
- RESTful API Design
- Pagination, Searching & Sorting
- Global Exception Handling
- Logging with Serilog
- Unit Testing using xUnit & Moq
- GitHub Actions CI/CD
- Docker Fundamentals

---

# 🤝 Contributing

Contributions, suggestions, and improvements are welcome.

If you would like to contribute:

1. Fork the repository.
2. Create a feature branch.
3. Commit your changes.
4. Open a Pull Request.

---


# 👨‍💻 Author

**Kunal Thakare**

- GitHub: https://github.com/<your-username>
- LinkedIn: https://www.linkedin.com/in/<your-linkedin>

---

# ⭐ Support

If you found this project useful, consider giving it a ⭐ on GitHub.

It helps others discover the project and motivates continued development.

---

## 📌 Project Summary

TradeMaster is an enterprise-grade trading and portfolio management backend built using ASP.NET Core Web API. The project follows Clean Architecture principles and demonstrates secure authentication, scalable application design, modern software engineering practices, automated testing, and CI/CD workflows. It serves as a production-inspired backend application for learning, portfolio development, and interview preparation.
