using Kitchen_Appliances_MVC.ViewModels.Category;

namespace Kitchen_Appliances_MVC.ViewModelData.Admin
{
	public class EditCategoryViewModel
	{
		public CategoryDTO Category { get; set; }

		public UpdateCategoryRequest request { get; set; }
	}
}
