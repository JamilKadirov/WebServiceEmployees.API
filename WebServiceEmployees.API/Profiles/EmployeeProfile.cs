using AutoMapper;
using WebServiceEmployees.API.Entities;
using WebServiceEmployees.API.Models;

namespace WebServiceEmployees.API.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
