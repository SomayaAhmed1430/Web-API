# ASP.NET Core Web API - Department & Employee Management

This project is a Web API built using **ASP.NET Core** and **Entity Framework Core**, supporting full CRUD operations on `Department` and `Employee` entities, as well as exposing useful **DTOs** for frontend consumption.

---

## 🎯 Features

- ASP.NET Core Web API using RESTful principles
- **CRUD for Departments and Employees**
- Use of **Entity Framework Core** with Code-First & Migrations
- Custom **DTOs** to return formatted data for consumers
- **CORS policy** added to allow external API consumers
- Tested via **Swagger UI** and **Postman**

---

## 📦 Entities

- `Department`
  - Id
  - Name
  - Description
- `Employee`
  - Id
  - Name
  - Address
  - DepartmentId (Foreign Key)

---

## 📤 DTOs Implemented

- `DeptWithEmp`: Shows Department ID, Name, and total number of Employees.
- `DeptWithEmps`: Shows Department Name with a list of Employee Names.
- `EmployeeWithDeptDTO`: Shows Employee Name and their Department Name.

---

## 🧪 Sample Endpoints

| Method | Route                            | Description                            |
|--------|----------------------------------|----------------------------------------|
| GET    | `api/Department`                 | Get all departments                    |
| GET    | `api/Department/{id}`            | Get department by ID                   |
| GET    | `api/Department/{name}`          | Get department by name                 |
| POST   | `api/Department`                 | Add new department                     |
| PUT    | `api/Department/{id}`            | Update department                      |
| DELETE | `api/Department/{id}`            | Delete department                      |
| GET    | `api/Department/D`               | Get list of `DeptWithEmp` DTOs         |
| GET    | `api/Department/WithNames`       | Get list of `DeptWithEmps` DTOs        |
| GET    | `api/Employee`                   | Get all employees with department name |

---

## 🔧 Technologies Used

- ASP.NET Core 6+
- Entity Framework Core
- SQL Server
- Swagger (Swashbuckle)
- Visual Studio 2022
- CORS for cross-domain access

---

## 🧪 How to Run & Test

1. Open project in Visual Studio
2. Update your connection string in `appsettings.json`
3. Run the project
4. Swagger UI will open at `https://localhost:{port}/swagger/index.html`
5. Use Swagger or Postman to test the endpoints

---

## 📝 Sample JSON for Department

```json
{
  "id": 1,
  "name": "Software",
  "description": "Tech Department"
}
