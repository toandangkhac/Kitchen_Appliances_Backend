using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Order;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IOrderRepository
    {

        //Danh sách order mà customer 
        Task<ApiResponse<List<OrderDTO>>> ListOrderByCustomer(int customerId);
        
        Task<ApiResponse<List<OrderDTO>>> ListOrderNotConfirm();

        //Danh sách các order để Nhân Viên Quản Lý
        Task<ApiResponse<List<OrderDTO>>> ListAllOrders();

        //Tạo order lấy tất cả CartDetail của Customer Tạo đơn hàng {trạng thái chờ thanh toán(1) }
        Task<ApiResponse<int>> CreateOrder(CreateOrderRequest request);

        //Xác nhận đơn hành tưf trạng thái chờ xác nhận(2) -> đang giao(3)
        Task<ApiResponse<bool>> ConfirmOrder(ConfirmOrderRequest request);

        //Người dùng chọn thanh toán khi nhận hàng
        Task<ApiResponse<bool>> ThanhToanKhiNhanHang(int orderId);
        //Người dùng vào Nút tiến hàng thanh toán Online VNPay

		//Người dùng Xác nhận đã giao hành thành công : trạng thái đang giao(3) -> đã nhận hàng (4)
		Task<ApiResponse<bool>> ConfirmOrderDeliverySucess(int orderId);
        //Hủy order đưa trạng thái về false
        //Nhân viên hủy đơn hàng
        Task<ApiResponse<bool>> CancelOrder(int orderId);
        //Khi mà các đơn đã có thời gian quá lâu muốn xóa
        Task<ApiResponse<bool>> DeleteOrder(int orderId);
    }
}
