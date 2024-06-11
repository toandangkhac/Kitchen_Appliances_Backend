using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.DTO.Employee;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<ApiResponse<object>> ActiveAccount(VerifyOTPRequest request);

        Task<ApiResponse<object>> ListEmployee();

        Task<ApiResponse<object>> GetEmployeeById(int id);

        Task<ApiResponse<object>> CreateEmployee(CreateEmployeeRequest request);

        Task<ApiResponse<object>> UpdateEmployee(int productId ,UpdateEmployeeRequest request);

        Task<ApiResponse<object>> DeleteEmployee(int id);

    }
}
