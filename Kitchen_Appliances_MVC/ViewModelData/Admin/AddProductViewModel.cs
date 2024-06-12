using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Product;

namespace Kitchen_Appliances_MVC.ViewModelData.Admin
{
	public class AddProductViewModel
	{
		public List<CategoryDTO> Categories { get; set; }

		public CreateProductRequest request { get; set; }
	}
}
