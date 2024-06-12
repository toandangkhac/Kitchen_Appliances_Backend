using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.ViewModels.VNPAY;

namespace Kitchen_Appliances_MVC.Abstractions
{
    public interface IVNPayClientService
    {
        //truyền vào một tham số Mã Đơn Hàng để thanh toán và tạo bill
        Task<APIResponse<string>> CreatePaymentUrl(int orderId);

        Task<APIResponse<VnPaymentResponseModel>> LoadDataPaymentSuccess();
    }
}
