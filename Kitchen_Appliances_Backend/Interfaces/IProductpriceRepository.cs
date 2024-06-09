using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.ProductPrice;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IProductPriceRepository
    {
        Task<ApiResponse<List<ProductPriceDTO>>> ListProductImageByProduct(int productId);

        Task<ApiResponse<bool>> UpdateProductPrice(UpdateProductPriceRequest request);
    }
}
