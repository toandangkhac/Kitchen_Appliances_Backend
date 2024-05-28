using Kitchen_Appliances_Backend.DTO.Category;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryById(int id);

        Task<bool> CreateCategory(CreateCategoryRequest request);

        Task<bool> UpdateCategory(int id ,UpdateCategoryRequest request);

        Task<bool> DeleteCategory(int id);

        List<Category> GetAllCategories();
    }
}
