using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.DTO;
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

        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeDTO>("/gateway/employee/" + $"{id}");
        }

        public async Task<List<EmployeeDTO>> GetListAll()
        {
           return await _httpClient.GetFromJsonAsync<List<EmployeeDTO>>("/gateway/employee");
        }

        public async Task<bool> CreateEmployee(CreateEmployeeRequest request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/gateway/employee/", request);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> UpdateEmployee(int productId, UpdateEmployeeRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("gateway/employee", request);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return false;
            }
            else
            {
                return true;
            }
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
