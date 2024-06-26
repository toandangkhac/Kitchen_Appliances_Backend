﻿using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.Customer;

namespace Kitchen_Appliances_MVC.ApiServices
{
    public class CustomerClientService : ICustomerServiceClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "/gateway/customer";
        //Đã TEST 
        public CustomerClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<APIResponse<List<CustomerDTO>>> ListCustomer()
        {
            return await _httpClient.GetFromJsonAsync<APIResponse<List<CustomerDTO>>>(BaseUrl);
        }

        public async Task<APIResponse<CustomerDTO>> GetCustomerById(int id)
        {
            return await _httpClient.GetFromJsonAsync<APIResponse<CustomerDTO>>(BaseUrl + $"/{id}");
        }

        public async Task<APIResponse<bool>> CreateCustomer(CreateCustomerRequest request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
            return await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
        }

        public async Task<APIResponse<bool>> UpdateCustomer(int id, UpdateCustomerRequest request)
        {
			HttpResponseMessage response = await _httpClient.PutAsJsonAsync(BaseUrl + $"/{id}", request);
			return await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
		}

        public async Task<APIResponse<bool>> DeleteCustomer(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(BaseUrl + $"/{id}");
            APIResponse<bool> result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
            return result;
        }
    }
}
