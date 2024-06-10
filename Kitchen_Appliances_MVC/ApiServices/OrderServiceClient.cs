using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.Customer;
using Kitchen_Appliances_MVC.ViewModels.Order;

namespace Kitchen_Appliances_MVC.ApiServices
{
	public class OrderServiceClient : IOrderServiceClient
	{
		private readonly HttpClient _httpClient;
		public const string BaseUrl = "/gateway/order";

		public OrderServiceClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public Task<APIResponse<bool>> CancelOrder(int orderId)
		{
			throw new NotImplementedException();
		}

		public Task<APIResponse<bool>> ConfirmOrder(int employeeId, int orderId)
		{
			throw new NotImplementedException();
		}

		public Task<APIResponse<bool>> ConfirmOrderDeliverySucess(int orderId)
		{
			throw new NotImplementedException();
		}

		public Task<APIResponse<bool>> ConfirmPaymentOrder(int orderId)
		{
			throw new NotImplementedException();
		}

		public Task<APIResponse<bool>> CreateOrder(CreateOrderRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<APIResponse<bool>> CreateOrderByListId(CreateOrderByListId request)
		{
			throw new NotImplementedException();
		}

		public Task<APIResponse<bool>> DeleteOrder(int orderId)
		{
			throw new NotImplementedException();
		}

		public async Task<APIResponse<List<OrderDTO>>> ListOrderByCustomer(int customerId)
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<List<OrderDTO>>>(BaseUrl + $"/get-order-by-customer/{customerId}");
		}

		public Task<APIResponse<List<OrderDTO>>> ListOrderConfirmed()
		{
			throw new NotImplementedException();
		}

		public Task<APIResponse<List<OrderDTO>>> ListOrderNotConfirm()
		{
			throw new NotImplementedException();
		}
	}
}
