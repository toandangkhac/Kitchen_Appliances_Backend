using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.Bill;
using Kitchen_Appliances_MVC.ViewModels.Customer;
using Microsoft.IdentityModel.Tokens;

namespace Kitchen_Appliances_MVC.ApiServices
{
	public class BillServiceClient : IBillServiceClient
	{
		private readonly HttpClient _httpClient;
		public const string BaseUrl = "/gateway/bill";

		public BillServiceClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		//Test thành công
		public async Task<APIResponse<BillDto>> GetBillInformation(int billId)
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<BillDto>>(BaseUrl + $"/get/{billId}");
		}

		public async Task<APIResponse<bool>> savePaymentInfor(int orderId)
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<bool>>(BaseUrl + $"/save/{orderId}");
		}

		public async Task<APIResponse<List<ListBillDto>>> GetAllBill()
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<List<ListBillDto>>>(BaseUrl + "/list");
		}
	}
}
