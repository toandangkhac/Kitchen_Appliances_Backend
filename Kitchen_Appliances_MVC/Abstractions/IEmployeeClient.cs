using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.ViewModels.Employee;

namespace Kitchen_Appliances_MVC.Abstractions
{
    public interface IEmployeeClient
    {
        Task<APIResponse<List<EmployeeDTO>>> GetListAll();

        //Task<EmployeeDTO> GetEmployeeById(int id);
        Task<APIResponse<EmployeeDTO>> GetEmployeeById(int id);

        Task<APIResponse<bool>> CreateEmployee(CreateEmployeeRequest request);

        Task<APIResponse<bool>> UpdateEmployee(int productId, UpdateEmployeeRequest request);

        Task<bool> DeleteEmployee(int id);

    }
}
