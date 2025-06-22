using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProviderAPI.DTO;
using ProviderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ProviderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ITIContext context;

        public EmployeeController(ITIContext _context)
        {
            context = _context;
        }

        [HttpGet("WithDept")]
        public ActionResult<List<EmpWithDept>> GetEmployeesWithDepartments()
        {
            var emps = context.Employees.Include(e => e.Department).ToList();

            var empDTOs = emps.Select(e => new EmpWithDept
            {
                Id = e.Id,
                EmpName = e.Name,
                DeptName = e.Department?.Name
            }).ToList();

            return Ok(empDTOs);
        }


        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) 
        {
            var emp = context.Employees
                             .Include(e => e.Department)
                             .FirstOrDefault(e => e.Id == id);

            if (emp == null)
                return NotFound();
            return Ok(emp);
        }


        [HttpPost]
        public IActionResult Add(AddNewEmp dto)
        {
            if (dto == null) return BadRequest();

            Employee emp = new Employee
            {
                Name = dto.Name,
                Address = dto.Address,
                DepartmentId = dto.DepartmentId
            };

            context.Employees.Add(emp);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = emp.Id }, emp);
        }



        [HttpPut("{id}")]
        public IActionResult Update(int id, Employee updatedEmp)
        {
            var emp = context.Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return NotFound();

            emp.Name = updatedEmp.Name;
            emp.Address = updatedEmp.Address;
            emp.DepartmentId = updatedEmp.DepartmentId;

            context.SaveChanges();

            return Ok(emp);
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var emp = context.Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return NotFound();

            context.Employees.Remove(emp);
            context.SaveChanges();

            return Ok("Deleted Successfully");
        }


    }
}
