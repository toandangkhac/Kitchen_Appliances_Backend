using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.ViewModels.ProductPrice;

namespace Kitchen_Appliances_MVC.Abstractions
{
	public interface IProductPriceServiceClient
	{
		Task<APIResponse<List<ProductPriceDTO>>> ListProductPriceByProduct(int productId);

		Task<APIResponse<bool>> UpdateProductPrice(UpdateProductPriceRequest request);
	}
}
