using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.CartItem;
namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface ICartDetailRepository
    {
        Task<ApiResponse<bool>> AddCartDetailToCart(CreateCartDetailRequest request);

        // không cần upload
        Task<ApiResponse<bool>> UpdateCartItemQuantity(UpdateCartDetailRequest request);

        Task<ApiResponse<bool>> DeleteCartDetail(GetCartDetailRequest request);

        //không sử dụng
        Task<ApiResponse<bool>> DeleteListCartDetail(List<int> ids);

        //Task<PaginatedResult<CartItemDto>> GetItemCartByUser(CartPagingRequest request);

        Task<ApiResponse<List<CartDetailDTO>>> GetCartDetailByCustomer(int customerId);


        Task<ApiResponse<CartDetailDTO>> GetCartDetail(GetCartDetailRequest request);
    }
}
