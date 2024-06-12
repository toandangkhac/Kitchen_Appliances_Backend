using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Kitchen_Appliances_MVC.ApiServices
{
	public class CategoryClientService : ICategoryServiceClient
	{

		private readonly HttpClient _httpClient;
		private const string BaseUrl = "/gateway/category";

		//Đã test
		public CategoryClientService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<APIResponse<CategoryDTO>> GetCategoryById(int id)
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<CategoryDTO>>(BaseUrl + $"/{id}");
		}

		public async Task<APIResponse<bool>> CreateCategory(CreateCategoryRequest request)
		{
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl, request);

			APIResponse<bool> result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
			return result;
		}

		public async Task<APIResponse<bool>> UpdateCategory(int id, UpdateCategoryRequest request)
		{
			HttpResponseMessage response = await _httpClient.PutAsJsonAsync(BaseUrl + $"/{id}", request);

			APIResponse<bool> result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
			return result;
		}

        public async Task<APIResponse<bool>> DeleteCategory(int id)
		{
			HttpResponseMessage response = await _httpClient.DeleteAsync(BaseUrl + $"/{id}");
			APIResponse<bool> result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
			return result;
		}

		public async Task<APIResponse<List<CategoryDTO>>> GetAllCategories()
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<List<CategoryDTO>>>(BaseUrl);
		}
	}
}
