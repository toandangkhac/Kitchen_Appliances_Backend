using AutoMapper;
using Kitchen_Appliances_Backend.DTO.Employee;
using Kitchen_Appliances_Backend.DTO.Role;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Helper
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Role, RoleDTO>();
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}
