using WebServiceEmployees.API.Models;

namespace WebServiceEmployees.API.Contracts
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDto> GetEmployee(int id);
        Task<int> AddEmployee(EmployeeForCreationDto employeeDto);
        Task DeleteEmployee(int id);
        Task<IEnumerable<EmployeeDto>> GetEmployeesByCompany(int companyId);
        Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartment(int companyId, int departmentId);
        Task UpdateEmployee(int id, EmployeeForUpdateDto employeeDto);
    }
}
