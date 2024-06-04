using Kitchen_Appliances_Backend.DTO.ProductPrice;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IProductPriceRepository
    {
        List<ProductPriceDTO> ListProductImageByProduct(int productId);

        Task<int> UpdateProductPrice(UpdateProductPriceRequest request);
    }
}
