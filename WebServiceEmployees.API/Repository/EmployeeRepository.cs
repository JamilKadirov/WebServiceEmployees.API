using AutoMapper;
using Dapper;
using System.Data;
using WebServiceEmployees.API.Context;
using WebServiceEmployees.API.Contracts;
using WebServiceEmployees.API.Entities;
using WebServiceEmployees.API.Models;

namespace WebServiceEmployees.API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(DapperContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            var query = "SELECT * FROM Employees WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QuerySingleOrDefaultAsync<EmployeeDto>(query, new { id });
                return employee;
            }
        }

        public async Task<int> AddEmployee(EmployeeForCreationDto employeeDto)
        {
            var query = "INSERT INTO Employees (Name, Surname, Phone, CompanyId, PassportId, DepartmentId) " +
                        "VALUES (@Name, @Surname, @Phone, @CompanyId, @PassportId, @DepartmentId); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS int);";

            var parameters = new DynamicParameters();
            parameters.Add("Name", employeeDto.Name, DbType.String);
            parameters.Add("Surname", employeeDto.Surname, DbType.String);
            parameters.Add("Phone", employeeDto.Phone, DbType.String);
            parameters.Add("CompanyId", employeeDto.CompanyId, DbType.Int32);
            parameters.Add("PassportId", employeeDto.Passport?.Id, DbType.Int32);
            parameters.Add("DepartmentId", employeeDto.Department?.Id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                return id;
            }
        }

        public async Task DeleteEmployee(int id)
        {
            var query = "DELETE FROM Employees WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByCompany(int companyId)
        {
            var query = @"SELECT e.*, p.*, d.* 
                  FROM Employees e 
                  JOIN Passports p ON e.PassportId = p.Id
                  JOIN Departments d ON e.DepartmentId = d.Id
                  WHERE e.CompanyId = @CompanyId";

            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<EmployeeDto, Passport, Department, EmployeeDto>(
                    query,
                    (e, p, d) => { e.Passport = p; e.Department = d; return e; },
                    new { companyId },
                    splitOn: "Id,Id"
                );
                return employees;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartment(int companyId, int departmentId)
        {
            var query = @"SELECT e.*, p.*, d.* 
                  FROM Employees e 
                  JOIN Passports p ON e.PassportId = p.Id
                  JOIN Departments d ON e.DepartmentId = d.Id
                  WHERE e.CompanyId = @CompanyId AND e.DepartmentId = @DepartmentId";

            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<EmployeeDto, Passport, Department, EmployeeDto>(
                    query,
                    (e, p, d) => { e.Passport = p; e.Department = d; return e; },
                    new { companyId, departmentId },
                    splitOn: "Id,Id"
                );
                return employees;
            }
        }

        public async Task UpdateEmployee(int id, EmployeeForUpdateDto employeeDto)
        {
            var query = @"DECLARE @NewPassportId INT;
                  DECLARE @NewDepartmentId INT;

                  IF NOT EXISTS (SELECT * FROM Passports WHERE Id = @PassportId)
                  BEGIN
                    INSERT INTO Passports (Type, Number) VALUES (@PassportType, @PassportNumber);
                    SET @NewPassportId = CAST(SCOPE_IDENTITY() AS int);
                  END
                  ELSE
                  BEGIN
                    UPDATE Passports SET Type = @PassportType, Number = @PassportNumber WHERE Id = @PassportId;
                    SET @NewPassportId = @PassportId;
                  END

                  IF NOT EXISTS (SELECT * FROM Departments WHERE Id = @DepartmentId)
                  BEGIN
                    INSERT INTO Departments (Name, Phone) VALUES (@DepartmentName, @DepartmentPhone);
                    SET @NewDepartmentId = CAST(SCOPE_IDENTITY() AS int);
                  END
                  ELSE
                  BEGIN
                    UPDATE Departments SET Name = @DepartmentName, Phone = @DepartmentPhone WHERE Id = @DepartmentId;
                    SET @NewDepartmentId = @DepartmentId;
                  END

                  UPDATE Employees SET Name = ISNULL(@Name, Name), Surname = ISNULL(@Surname, Surname), Phone = ISNULL(@Phone, Phone), CompanyId = ISNULL(@CompanyId, CompanyId), PassportId = ISNULL(@NewPassportId, PassportId), DepartmentId = ISNULL(@NewDepartmentId, DepartmentId) 
                  WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", employeeDto.Name, DbType.String);
            parameters.Add("Surname", employeeDto.Surname, DbType.String);
            parameters.Add("Phone", employeeDto.Phone, DbType.String);
            parameters.Add("CompanyId", employeeDto.CompanyId, DbType.Int32);
            parameters.Add("PassportId", employeeDto.Passport?.Id, DbType.Int32);
            parameters.Add("PassportType", employeeDto.Passport?.Type, DbType.String);
            parameters.Add("PassportNumber", employeeDto.Passport?.Number, DbType.String);
            parameters.Add("DepartmentId", employeeDto.Department?.Id, DbType.Int32);
            parameters.Add("DepartmentName", employeeDto.Department?.Name, DbType.String);
            parameters.Add("DepartmentPhone", employeeDto.Department?.Phone, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
