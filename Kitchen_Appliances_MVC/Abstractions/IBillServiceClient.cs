using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.ViewModels.Bill;

namespace Kitchen_Appliances_MVC.Abstractions
{
	public interface IBillServiceClient
	{
		//Tạo bill hóa đơn cho đơn hàng
		Task<APIResponse<bool>> savePaymentInfor(int orderId);

		//In ra thông tin hóa đơn
		Task<APIResponse<BillDto>> GetBillInformation(int billId);

		//In danh sách hóa đơn
		Task<APIResponse<List<ListBillDto>>> GetAllBill();
	}
}
