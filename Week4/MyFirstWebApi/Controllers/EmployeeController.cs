using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,POC")] 
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John", Salary = 50000, Permanent = true, Department = new Department { Id = 1, Name = "HR" }, Skills = new List<Skill> { new Skill { Id = 1, Name = "C#" } } },
            new Employee { Id = 2, Name = "Alice", Salary = 60000, Permanent = false, Department = new Department { Id = 2, Name = "IT" }, Skills = new List<Skill> { new Skill { Id = 2, Name = "Java" } } },
            new Employee { Id = 3, Name = "Bob", Salary = 70000, Permanent = true, Department = new Department { Id = 3, Name = "Finance" }, Skills = new List<Skill> { new Skill { Id = 3, Name = "SQL" } } }
        };

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return Ok(employees);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, [FromBody] Employee updatedEmployee)
        {
            if (id <= 0)
                return BadRequest("Invalid employee id");

            var existingEmployee = employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
                return BadRequest("Invalid employee id");

            existingEmployee.Name = updatedEmployee.Name;
            existingEmployee.Salary = updatedEmployee.Salary;
            existingEmployee.Permanent = updatedEmployee.Permanent;
            existingEmployee.Department = updatedEmployee.Department;
            existingEmployee.Skills = updatedEmployee.Skills;

            return Ok(existingEmployee);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return NotFound($"Employee with id {id} not found.");

            employees.Remove(emp);
            return Ok($"Employee with id {id} deleted.");
        }
    }
}
