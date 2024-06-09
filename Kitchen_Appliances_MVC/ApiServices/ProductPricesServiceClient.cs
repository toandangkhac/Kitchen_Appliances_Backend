using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.ProductPrice;

namespace Kitchen_Appliances_MVC.ApiServices
{
	public class ProductPricesServiceClient : IProductPriceServiceClient
	{
		private readonly HttpClient _httpClient;
		private const string BaseUrl = "/gateway/productprice";

		//Đã test ổn
		public ProductPricesServiceClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<APIResponse<List<ProductPriceDTO>>> ListProductPriceByProduct(int productId)
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<List<ProductPriceDTO>>>(BaseUrl +  $"/{productId}");
		}

		public async Task<APIResponse<bool>> UpdateProductPrice(UpdateProductPriceRequest request)
		{
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl, request);

			APIResponse<bool> result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
			return result;
		}
	}
}
