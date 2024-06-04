

using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.ViewModels.Customer;

namespace Kitchen_Appliances_MVC.Abstractions
{
    public interface ICustomerServiceClient
    {
        Task<APIResponse<List<CustomerDTO>>> ListCustomer();

        Task<APIResponse<CustomerDTO>> GetCustomerById(int id);

        Task<APIResponse<bool>> CreateCustomer(CreateCustomerRequest request);

        Task<APIResponse<bool>> UpdateCustomer(int id, UpdateCustomerRequest request);

        Task<APIResponse<bool>> DeleteCustomer(int id);
    }
}
