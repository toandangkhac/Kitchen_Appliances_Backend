using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.Employee;

namespace Kitchen_Appliances_MVC.ApiServices
{
    public class EmployeeClientService : IEmployeeClient
    {
        public HttpClient _httpClient;

        public EmployeeClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //Đã test
        public async Task<APIResponse<EmployeeDTO>> GetEmployeeById(int id)
        {
            return await _httpClient.GetFromJsonAsync<APIResponse<EmployeeDTO>>("/gateway/employee/" + $"{id}");
        }

        public async Task<APIResponse<List<EmployeeDTO>>> GetListAll()
        {
           return await _httpClient.GetFromJsonAsync<APIResponse<List<EmployeeDTO>>>("/gateway/employee/");
        }

        public async Task<APIResponse<bool>> CreateEmployee(CreateEmployeeRequest request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/gateway/employee", request);
            APIResponse<bool> result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
            return result;
 
        }

        public async Task<APIResponse<bool>> UpdateEmployee(int productId, UpdateEmployeeRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("gateway/employee", request);

            APIResponse<bool> result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
            return result;
 
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync("/gateway/employee/" + $"{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
