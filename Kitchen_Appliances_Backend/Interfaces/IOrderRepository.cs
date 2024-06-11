using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Order;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IOrderRepository
    {

        //Danh sách order mà customer đã thanh toán
        Task<ApiResponse<List<OrderDTO>>> ListOrderByCustomer(int customerId);
        
        Task<ApiResponse<List<OrderDTO>>> ListOrderNotConfirm();

        Task<ApiResponse<List<OrderDTO>>> ListOrderConfirmed();

        //Tạo order thông qua List CartDetail (thông thể dùng đc hazzz)
        Task<ApiResponse<bool>> CreateOrderByListId(CreateOrderByListId request);

        //Tạo order lấy tất cả CartDetail của Customer
        Task<ApiResponse<bool>> CreateOrder(CreateOrderRequest request);
        //Xác nhận đơn hành tưf trạng thái chờ xác nhận(1) -> đang giao(2)
        Task<ApiResponse<bool>> ConfirmOrder(ConfirmOrderRequest request);
		//Xác nhận đơn hàng đã thành toán
		Task<ApiResponse<bool>> ConfirmPaymentOrder(int orderId);

		//Xác nhận đã giao hành thành công : trạng thái đang giao(2) -> đã nhận hàng (3)
		Task<ApiResponse<bool>> ConfirmOrderDeliverySucess(int orderId);
        //Hủy order đưa trạng thái về false
        Task<ApiResponse<bool>> CancelOrder(int orderId);
        //Khi mà các đơn đã có thời gian quá lâu muốn xóa
        Task<ApiResponse<bool>> DeleteOrder(int orderId);
    }
}
