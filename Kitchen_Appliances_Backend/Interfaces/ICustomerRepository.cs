using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.DTO.Customer;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface ICustomerRepository
    {
        //ICollection<CustomerDTO> ListCustomer();
        Task<ApiResponse<List<CustomerDTO>>> ListCustomer();

        //Task<CustomerDTO> GetCustomerById(int id);
        Task<ApiResponse<CustomerDTO>> GetCustomerById(int id);

        //Task<bool> CreateCustomer(CreateCustomerRequest request);
        Task<ApiResponse<bool>> CreateCustomer(CreateCustomerRequest request);

        //Task<bool> UpdateCustomer(int id, UpdateCustomerRequest request);
        Task<ApiResponse<bool>> UpdateCustomer(int id, UpdateCustomerRequest request);

        //Task<bool> DeleteCustomer(int id);
        Task<ApiResponse<bool>> DeleteCustomerById(int id);
    }
}
