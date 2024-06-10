using Kitchen_Appliances_Backend.Commons.Responses;

namespace Kitchen_Appliances_MVC.Abstractions
{
    public interface IVNPayClientService
    {
        Task<APIResponse<string>> CreatePaymentUrl();
    }
}
