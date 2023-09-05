using WebServiceEmployees.API.Models;

namespace WebServiceEmployees.API.Contracts
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetEmployeeAsync(int id);
        Task<int> AddEmployeeAsync(EmployeeForCreationDto employeeDto);
        Task DeleteEmployeeAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetEmployeesByCompanyAsync(int companyId);
        Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentAsync(int companyId, int departmentId);
        Task UpdateEmployeeAsync(int id, EmployeeForUpdateDto employeeDto);
    }
}
