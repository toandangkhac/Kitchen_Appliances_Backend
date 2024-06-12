using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.ViewModels.Order;

namespace Kitchen_Appliances_MVC.Abstractions
{
	public interface IOrderServiceClient
	{
        //Danh sách order mà customer 
        Task<APIResponse<List<OrderDTO>>> ListOrderByCustomer(int customerId);

        Task<APIResponse<List<OrderDTO>>> ListOrderNotConfirm();

        //Danh sách các order để Nhân Viên Quản Lý
        Task<APIResponse<List<OrderDTO>>> ListAllOrders();

        //Tạo order lấy tất cả CartDetail của Customer Tạo đơn hàng {trạng thái chờ thanh toán(1) }
        Task<APIResponse<int>> CreateOrder(CreateOrderRequest request);


        //Xác nhận đơn hành tưf trạng thái chờ xác nhận(2) -> đang giao(3)
        Task<APIResponse<bool>> ConfirmOrder(ConfirmOrderRequest request);


        //Người dùng chọn thanh toán khi nhận hàng
        Task<APIResponse<bool>> ThanhToanKhiNhanHang(int orderId);
        //Người dùng vào Nút tiến hàng thanh toán Online VNPay

        //Người dùng Xác nhận đã giao hành thành công : trạng thái đang giao(3) -> đã nhận hàng (4)
        Task<APIResponse<bool>> ConfirmOrderDeliverySucess(int orderId);
        //Hủy order đưa trạng thái về false
        //Nhân viên hủy đơn hàng
        Task<APIResponse<bool>> CancelOrder(int orderId);
        //Khi mà các đơn đã có thời gian quá lâu muốn xóa
        Task<APIResponse<bool>> DeleteOrder(int orderId);
    }
}
