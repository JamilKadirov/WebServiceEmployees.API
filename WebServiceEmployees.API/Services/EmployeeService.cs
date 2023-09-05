using WebServiceEmployees.API.Contracts;
using WebServiceEmployees.API.Models;

namespace WebServiceEmployees.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeService(IEmployeeRepository employeeRepo) => _employeeRepo = employeeRepo;

        public async Task<EmployeeDto> GetEmployeeAsync(int id)
        {
            var employee = await _employeeRepo.GetEmployee(id);
            return employee;
        }

        public async Task<int> AddEmployeeAsync(EmployeeForCreationDto employeeDto)
        {
            var newEmployeeId = await _employeeRepo.AddEmployee(employeeDto);
            return newEmployeeId;
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeRepo.DeleteEmployee(id);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByCompanyAsync(int companyId)
        {
            var employees = await _employeeRepo.GetEmployeesByCompany(companyId);
            return employees;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentAsync(int companyId, int departmentId)
        {
            var employees = await _employeeRepo.GetEmployeesByDepartment(companyId, departmentId);
            return employees;
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeForUpdateDto employeeDto)
        {
            await _employeeRepo.UpdateEmployee(id, employeeDto);
        }
    }
}
