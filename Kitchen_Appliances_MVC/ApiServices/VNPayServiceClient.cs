using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.VNPAY;

namespace Kitchen_Appliances_MVC.ApiServices
{
    public class VNPayServiceClient : IVNPayClientService
    {
        private readonly HttpClient _httpClient;
        public VNPayServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<APIResponse<string>> CreatePaymentUrl(int orderId)
        {
            return await _httpClient.GetFromJsonAsync<APIResponse<string>>("/gateway/vnpay/" + $"{orderId}");
        }

        public async Task<APIResponse<VnPaymentResponseModel>> LoadDataPaymentSuccess()
        {
            return await _httpClient.GetFromJsonAsync<APIResponse<VnPaymentResponseModel>>("/gateway/vnpay/call-back");
        }
    }
}
