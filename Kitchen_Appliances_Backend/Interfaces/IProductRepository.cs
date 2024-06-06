using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Product;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IProductRepository
    {

        //Task<ProductDTO> GetProductById(int id);
        Task<ApiResponse<ProductDTO>> GetProductById(int id);

        Task<ApiResponse<bool>> CreateProduct(CreateProductRequest request);

        Task<ApiResponse<bool>> UpdateProduct(int id, UpdateProductRequest request);

        Task<ApiResponse<bool>> DeleteProduct(int id);

        Task<ApiResponse<List<ProductDTO>>> GetAllProducts();

        Task<ApiResponse<List<ProductDTO>>> ListProductByCategory(int categoryId);
    }
}
