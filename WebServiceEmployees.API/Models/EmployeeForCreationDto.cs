using WebServiceEmployees.API.Entities;

namespace WebServiceEmployees.API.Models;

public class EmployeeForCreationDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Phone { get; set; }
    public int? CompanyId { get; set; }
    public Passport? Passport { get; set; }
    public Department? Department { get; set; }
}
