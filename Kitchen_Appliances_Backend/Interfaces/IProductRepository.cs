using Kitchen_Appliances_Backend.DTO.Product;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IProductRepository
    {

        Task<ProductDTO> GetProductById(int id);

        Task<bool> CreateProduct(CreateProductRequest request);

        Task<bool> UpdateProduct(int id, UpdateProductRequest request);

        Task<bool> DeleteProduct(int id);

        List<ProductDTO> GetAllProducts();
    }
}
