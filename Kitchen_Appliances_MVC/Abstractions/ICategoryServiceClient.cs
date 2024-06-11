using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.ViewModels.Category;

namespace Kitchen_Appliances_MVC.Abstractions
{
    public interface ICategoryServiceClient
	{
		Task<APIResponse<CategoryDTO>> GetCategoryById(int id);

		//Task<bool> CreateCategory(CreateCategoryRequest request);
		Task<APIResponse<bool>> CreateCategory(CreateCategoryRequest request);

		//Task<bool> UpdateCategory(int id ,UpdateCategoryRequest request);
		Task<APIResponse<bool>> UpdateCategory(int id, UpdateCategoryRequest request);

		//Task<bool> DeleteCategory(int id);
		Task<APIResponse<bool>> DeleteCategory(int id);


		//List<Category> GetAllCategories();
		Task<APIResponse<List<CategoryDTO>>> GetAllCategories();
	}
}
