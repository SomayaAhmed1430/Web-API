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
    }
}
