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

        
        Task<ApiResponse<bool>> CreateOrder(CreateOrderRequest request);

        //Hủy order đưa trạng thái về false
        Task<ApiResponse<bool>> CancelOrder(int orderId);



    }
}
