using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.OrderDetail;

namespace Kitchen_Appliances_MVC.ApiServices
{
	public class OrderDetailServiceClient : IOrderDetailServiceClient
	{
		private readonly HttpClient _httpClient;
		public const string BaseUrl = "/gateway/orderdetail";
		public OrderDetailServiceClient(HttpClient httpClient) { 
			_httpClient = httpClient;
		}
		public async Task<APIResponse<List<OrderDetailDTO>>> GetAllOrderDetailsByOrder(int orderId)
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<List<OrderDetailDTO>>>(BaseUrl + $"/list-order-detail/{orderId}");
		}

		public async Task<APIResponse<OrderDetailDTO>> GetOrderDetailById(int orderDetailId)
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<OrderDetailDTO>>(BaseUrl + $"/get-order-detail/{orderDetailId}");
		}
	}
}
