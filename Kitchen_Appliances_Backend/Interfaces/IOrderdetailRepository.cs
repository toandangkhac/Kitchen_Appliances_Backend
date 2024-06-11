using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.OrderDetail;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IOrderdetailRepository
    {
        Task<bool> CreateOrderDetails(List<int> cartDetailIds);

        Task<ApiResponse<OrderDetailDTO>> GetOrderDetailById(int orderDetailId);

        Task<ApiResponse<List<OrderDetailDTO>>> GetAllOrderDetailsByOrder(int orderId);
    }
}
