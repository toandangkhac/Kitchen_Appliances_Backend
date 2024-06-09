using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.ViewModels.Order;

namespace Kitchen_Appliances_MVC.Abstractions
{
	public interface IOrderServiceClient
	{
		//Danh sách order mà customer đã thanh toán
		Task<APIResponse<List<OrderDTO>>> ListOrderByCustomer(int customerId);

		Task<APIResponse<List<OrderDTO>>> ListOrderNotConfirm();

		Task<APIResponse<List<OrderDTO>>> ListOrderConfirmed();

		//Tạo order thông qua List CartDetail (thông thể dùng đc hazzz)
		Task<APIResponse<bool>> CreateOrderByListId(CreateOrderByListId request);

		//Tạo order lấy tất cả CartDetail của Customer
		Task<APIResponse<bool>> CreateOrder(CreateOrderRequest request);
		Task<APIResponse<bool>> ConfirmOrder(int employeeId, int orderId);
		//Xác nhận đơn hàng đã thành toán
		Task<APIResponse<bool>> ConfirmPaymentOrder(int orderId);

		//Xác nhận đã giao hành thành công
		Task<APIResponse<bool>> ConfirmOrderDeliverySucess(int orderId);
		//Hủy order đưa trạng thái về false
		Task<APIResponse<bool>> CancelOrder(int orderId);
		//Khi mà các đơn đã có thời gian quá lâu muốn xóa
		Task<APIResponse<bool>> DeleteOrder(int orderId);
	}
}
