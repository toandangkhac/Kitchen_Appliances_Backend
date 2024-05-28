using AutoMapper;
using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.DTO.Category;
using Kitchen_Appliances_Backend.DTO.Employee;
using Kitchen_Appliances_Backend.DTO.Product;
using Kitchen_Appliances_Backend.DTO.Role;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Helper
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Role, RoleDTO>();

            //Employee
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<CreateEmployeeRequest, Employee>();
            CreateMap<UpdateEmployeeRequest, Employee>();

            //Category
            CreateMap<Category, CategoryDTO>();
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<UpdateCategoryRequest, Category>();

            //Account
            CreateMap<Account, AccountDTO>();

            //Product
            CreateMap<Product, ProductDTO>();
        }
    }
}
