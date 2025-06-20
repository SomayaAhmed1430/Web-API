using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProviderAPI.DTO;
using ProviderAPI.Models;

namespace ProviderAPI.Controllers
{

    [Route("api/[controller]")]  // api/Department
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        ITIContext context;
        public DepartmentController(ITIContext context)
        {
            this.context = context;
        }


        [HttpGet]  // Get: api/Department
        public IActionResult DisplayAllDept()
        {
            List<Department> deptList = context.Departments.ToList();
            return Ok(deptList);
        }


        [HttpGet]
        [Route("{id:int}")]  // Get: api/Department/1
        public IActionResult GetById(int id)
        {
            Department department = context.Departments.FirstOrDefault(x => x.Id == id);
            if (department == null)
                return NotFound();
            return Ok(department);
        }


        [HttpGet]
        [Route("{name:alpha}")]  // Get: api/Department/value
        public IActionResult GetByName(string name)
        {
            Department department = context.Departments.FirstOrDefault(x => x.Name == name);
            if (department == null)
                return NotFound();
            return Ok(department);
        }


        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();

            return CreatedAtAction("GetById", new { id = department.Id }, department);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Department updatedDept)
        {
            var dept = context.Departments.FirstOrDefault(d => d.Id == id);
            if (dept == null)
            {
                return NotFound();
            }

            dept.Name = updatedDept.Name;
            dept.Description = updatedDept.Description;

            context.SaveChanges();
            return Ok(dept);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dept = context.Departments.FirstOrDefault(d => d.Id == id);
            if (dept == null)
            {
                return NotFound();
            }

            context.Departments.Remove(dept);
            context.SaveChanges();

            return Ok("Deleted successfully");
        }

        [HttpGet("EmpCount")]
        public ActionResult<List<DeptWithEmp>> GetDeptDetails()
        {
            List<Department> depts = context.Departments.Include(d => d.Employees).ToList();

            List<DeptWithEmp> DepptListDTO = new List<DeptWithEmp>();

            foreach (var item in depts)
            {
                DeptWithEmp deptDTO = new DeptWithEmp();
                deptDTO.DeptId = item.Id;
                deptDTO.DeptName = item.Name;
                deptDTO.EmpCount = item.Employees.Count.ToString();

                DepptListDTO.Add(deptDTO);
            }
            return Ok(DepptListDTO);
        }


        [HttpGet("WithEmployees")]
        public ActionResult<List<DeptWithEmps>> GetDeptsWithEmployees()
        {
            var depts = context.Departments.Include(d => d.Employees).ToList();

            var deptDTOs = depts.Select(d => new DeptWithEmps
            {
                DeptName = d.Name,
                EmpNames = d.Employees.Select(e => e.Name).ToList()
            }).ToList();

            return Ok(deptDTOs);
        }

    }
}
