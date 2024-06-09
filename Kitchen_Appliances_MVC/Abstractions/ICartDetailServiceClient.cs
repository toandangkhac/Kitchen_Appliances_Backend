using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.ViewModels.CartDetail;

namespace Kitchen_Appliances_MVC.Abstractions
{
	public interface ICartDetailServiceClient
	{
		Task<APIResponse<bool>> DeleteCartDetail(GetCartDetailRequest request);

		Task<APIResponse<CartDetailDTO>> GetCartDetail(GetCartDetailRequest request);

		Task<APIResponse<bool>> AddCartDetailToCart(CreateCartDetailRequest request);

		Task<APIResponse<List<CartDetailDTO>>> GetCartDetailByCustomer(int customerId);
	}
}
