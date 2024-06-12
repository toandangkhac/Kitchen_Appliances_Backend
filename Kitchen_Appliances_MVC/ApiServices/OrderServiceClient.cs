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
		public async Task<APIResponse<bool>> CancelOrder(int orderId)
		{
			HttpResponseMessage response = await _httpClient.PutAsync(BaseUrl + $"/cancel-order/{orderId}",null);
			return await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
		}

		public async Task<APIResponse<bool>> ConfirmOrderDeliverySucess(int orderId)
		{
			HttpResponseMessage response = await _httpClient.PutAsync(BaseUrl + $"/confirm-delivery-success/{orderId}", null);
			return await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
		}

		public async Task<APIResponse<int>> CreateOrder(CreateOrderRequest request)
		{
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl + $"/create-order-by-customer", request);
			return await response.Content.ReadFromJsonAsync<APIResponse<int>>();
		}

		public Task<APIResponse<bool>> DeleteOrder(int orderId)
		{
			throw new NotImplementedException();
		}

		public async Task<APIResponse<List<OrderDTO>>> ListOrderByCustomer(int customerId)
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<List<OrderDTO>>>(BaseUrl + $"/get-order-by-customer/{customerId}");
		}

		public async Task<APIResponse<List<OrderDTO>>> ListOrderNotConfirm()
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<List<OrderDTO>>>(BaseUrl + "/list-order-not-confirm");
		}

        public async Task<APIResponse<List<OrderDTO>>> ListAllOrders()
        {
			return await _httpClient.GetFromJsonAsync<APIResponse<List<OrderDTO>>>(BaseUrl + $"/list-all-order");
		}

		public async Task<APIResponse<bool>> ConfirmOrder(ConfirmOrderRequest request)
        {
			HttpResponseMessage response = await _httpClient.PutAsJsonAsync(BaseUrl + $"/confirm-order-by-employee", request);
			return await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
		}

        public Task<APIResponse<bool>> ThanhToanKhiNhanHang(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
