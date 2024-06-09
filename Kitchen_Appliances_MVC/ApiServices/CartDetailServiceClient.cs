using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.CartDetail;
using Kitchen_Appliances_MVC.ViewModels.Customer;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

namespace Kitchen_Appliances_MVC.ApiServices
{
	public class CartDetailServiceClient : ICartDetailServiceClient
	{
		private readonly HttpClient _httpClient;
		private readonly string BaseUrl = "/gateway/cartdetail";

		public CartDetailServiceClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<APIResponse<bool>> AddCartDetailToCart(CreateCartDetailRequest request)
		{
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
			APIResponse<bool> result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
			return result;
		}

		public async Task<APIResponse<bool>> DeleteCartDetail(GetCartDetailRequest request)
		{
			HttpResponseMessage response = await _httpClient.PutAsJsonAsync("/gateway/cartdetail/delete", request);
			APIResponse<bool> result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
			return result;
		}

		//Test thành công
		public async Task<APIResponse<CartDetailDTO>> GetCartDetail(GetCartDetailRequest request)
		{
			// muốn truyền object thông qua httpclient bắt buộc sử dụng method post
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl + "/find-by-id", request);

			APIResponse<CartDetailDTO> result = await response.Content.ReadFromJsonAsync<APIResponse<CartDetailDTO>>();
			return result;
		}
		//Test thành công
		public async Task<APIResponse<List<CartDetailDTO>>> GetCartDetailByCustomer(int customerId)
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<List<CartDetailDTO>>> (BaseUrl + $"/{customerId}");
		}
	}
}
