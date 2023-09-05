using Microsoft.AspNetCore.Mvc;
using WebServiceEmployees.API.Contracts;
using WebServiceEmployees.API.Models;

namespace WebServiceEmployees.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService) =>
            _employeeService = employeeService;

        [HttpGet("companies/{companyId}/getemployees")]
        public async Task<IActionResult> GetEmployeesByCompany(int companyId)
        {
            var employees = await _employeeService.GetEmployeesByCompanyAsync(companyId);
            if (!employees.Any())
                return NotFound();

            return Ok(employees);
        }

        [HttpGet("companies/{companyId}/departments/{departmentId}/getemployees")]
        public async Task<IActionResult> GetEmployeesByDepartment(int companyId, int departmentId)
        {
            var employees = await _employeeService.GetEmployeesByDepartmentAsync(companyId, departmentId);
            if (!employees.Any())
                return NotFound();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployee([FromBody] EmployeeForCreationDto employeeDto)
        {
            var employeeId = await _employeeService.AddEmployeeAsync(employeeDto);
            return Ok(employeeId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id,
            [FromBody] EmployeeForUpdateDto employeeDto)
        {
            var dbEmployee = await _employeeService.GetEmployeeAsync(id);
            if (dbEmployee == null)
                return NotFound();

            await _employeeService.UpdateEmployeeAsync(id, employeeDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var dbEmployee = await _employeeService.GetEmployeeAsync(id);
            if (dbEmployee == null)
                return NotFound();

            await _employeeService.DeleteEmployeeAsync(id);

            return NoContent();
        }
    }
}
