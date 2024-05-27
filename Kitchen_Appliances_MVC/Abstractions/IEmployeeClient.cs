using Kitchen_Appliances_MVC.DTO;
using Kitchen_Appliances_MVC.ViewModels.Employee;

namespace Kitchen_Appliances_MVC.Abstractions
{
    public interface IEmployeeClient
    {
        Task<List<EmployeeDTO>> GetListAll();

        Task<EmployeeDTO> GetEmployeeById(int id);

        Task<bool> CreateEmployee(CreateEmployeeRequest request);

        Task<bool> UpdateEmployee(int productId, UpdateEmployeeRequest request);

        Task<bool> DeleteEmployee(int id);

    }
}
