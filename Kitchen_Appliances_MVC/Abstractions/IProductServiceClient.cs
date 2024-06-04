using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.ViewModels.Product;

namespace Kitchen_Appliances_MVC.Abstractions
{
    public interface IProductServiceClient
    {
        Task<APIResponse<ProductDTO>> GetProductById(int id);

        Task<APIResponse<bool>> CreateProduct(CreateProductRequest request);

        Task<APIResponse<bool>> UpdateProduct(int id, UpdateProductRequest request);

        Task<APIResponse<bool>> DeleteProduct(int id);

        Task<APIResponse<List<ProductDTO>>> GetAllProducts();

        Task<APIResponse<List<ProductDTO>>> ListProductByCategory(int categoryId);
    }
}
