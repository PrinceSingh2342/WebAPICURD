using EmloyeeAdminPortal.Data;
using EmloyeeAdminPortal.Models;
using EmloyeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmloyeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase

    {
        
        private readonly ApplicationDBContext dBContext;

        public EmployeesController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dBContext.Employees.ToList();
            return Ok(allEmployees);

        }
        [HttpGet]
        [Route("id:guid")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dBContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
            [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployee)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployee.Name,
                Email = addEmployee.Email,
                Phone = addEmployee.Phone,
                Salary = addEmployee.Salary
            };
            dBContext.Employees.Add(employeeEntity);
            dBContext.SaveChanges();
            return Ok(employeeEntity);
        }
        [HttpPut]
        [Route("id:guid")]
        public IActionResult UpdateEmployee(Guid id,UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dBContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;
            dBContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("id:guid")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dBContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            dBContext.Employees.Remove(employee);
            dBContext.SaveChanges();
            return Ok();
        }
    }
}
