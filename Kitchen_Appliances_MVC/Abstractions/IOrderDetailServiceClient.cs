using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.ViewModels.OrderDetail;

namespace Kitchen_Appliances_MVC.Abstractions
{
	public interface IOrderDetailServiceClient
	{
		Task<APIResponse<OrderDetailDTO>> GetOrderDetailById(int orderDetailId);
		Task<APIResponse<List<OrderDetailDTO>>> GetAllOrderDetailsByOrder(int orderId);
	}
}
