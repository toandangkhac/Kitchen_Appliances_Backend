using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Role;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.Product;

namespace Kitchen_Appliances_MVC.ApiServices
{
	public class RoleServiceClient : IRoleServiceClient
	{
		private readonly HttpClient _httpClient;
		private const string BaseUrl = "/gateway/role";
		public RoleServiceClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<APIResponse<List<RoleDTO>>> GetRoles()
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<List<RoleDTO>>>(BaseUrl);
		}
	}
}
