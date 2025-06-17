# ASP.NET Core Web API - Department Management

This project is a simple Web API built using **ASP.NET Core** and **Entity Framework Core**, allowing basic CRUD operations on a `Department` entity.

## 🎯 Features

- ASP.NET Core Web API using RESTful conventions
- Full CRUD support:
  - `GET` all departments
  - `GET` by ID or by Name
  - `POST` to create a new department
  - `PUT` to update an existing department
  - `DELETE` to remove a department
- Routing with constraints (`int`, `alpha`)
- Tested using **Swagger** and **Postman**
- JSON request and response formatting

## 🔧 Technologies Used

- ASP.NET Core 6+
- Entity Framework Core
- SQL Server
- Swagger (Swashbuckle)
- Visual Studio 2022

## 📁 Endpoints Overview

| Method | Route                        | Description                |
|--------|------------------------------|----------------------------|
| GET    | `api/Department`             | Get all departments        |
| GET    | `api/Department/{id}`        | Get department by ID       |
| GET    | `api/Department/{name}`      | Get department by Name     |
| POST   | `api/Department`             | Add a new department       |
| PUT    | `api/Department/{id}`        | Update a department        |
| DELETE | `api/Department/{id}`        | Delete a department        |

## 🧪 How to Test

- Run the project using Visual Studio
- The Swagger UI will open automatically
- You can test all endpoints directly from Swagger
- Alternatively, use Postman to test requests manually (Content-Type: `application/json`)

## 📌 Sample JSON for POST/PUT

```json
{
  "id": 1,
  "name": "Software",
  "description": "Tech department"
}
