# 🛒 ApiEcommerce - Clean Architecture .NET 8

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![SQL Server](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-black?style=for-the-badge&logo=JSON%20web%20tokens)

A robust and scalable E-commerce REST API built with **.NET 8**, focusing on high-quality software engineering standards and **Clean Architecture** principles.

## 🏗️ Architecture & Design Patterns
The project is divided into four main layers to ensure separation of concerns and maintainability:

- **Domain:** Core entities, interfaces, and business logic.
- **Application:** DTOs, Mapping (Mapster), and Application interfaces.
- **Infrastructure:** Data persistence (Entity Framework), Repository implementation, and Identity configuration.
- **API (Presentation):** Controllers, Middleware, and Swagger documentation.


## ✨ Features
- **Authentication & Authorization:** Secure access using **ASP.NET Core Identity** and **JWT Bearer Tokens**.
- **Data Persistence:** SQL Server integration with **Entity Framework Core**.
- **API Versioning:** Support for multiple API versions (v1, v2) using `Asp.Versioning`.
- **Caching:** Optimized performance with Response Caching.
- **Documentation:** Interactive API exploration with **Swagger UI**.


## 🛠️ Tech Stack
- **Language:** C# / .NET 8
- **ORM:** Entity Framework Core
- **Database:** Microsoft SQL Server
- **Mapping:** Mapster
- **Containers:** Docker Support
- **Security:** Identity JWT

## 🚥 Get Started

1. **Clone the repository:**
<bash>
   git clone [https://github.com/Agustin8090/ApiEcommerce.git](https://github.com/Agustin8090/ApiEcommerce.git)
<bash>

2. **Update Connection String:**

Update Connection String: Modify appsettings.json in the Web API project with your SQL Server credentials.


3. **Run Migrations:**

<bash>
dotnet ef database update --project ApiEcommerce.Infrastructure --startup-project ApiEcommerce
<bash>

4. **Run the App:**

<bash>
dotnet run --project ApiEcommerce.Web
<bash>