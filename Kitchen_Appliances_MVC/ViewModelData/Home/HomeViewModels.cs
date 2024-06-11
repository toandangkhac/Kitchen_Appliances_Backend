using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Image;
using Kitchen_Appliances_MVC.ViewModels.Product;

namespace Kitchen_Appliances_MVC.ViewModelData.Home
{
	public class HomeViewModels
	{
		public List<CategoryDTO> Categories { get; set; }
		public List<ProductDTO> Products { get; set; }	
		public List<ImageDTO> Images { get; set; }

		public CategoryDTO Category { get; set; } = null;
	}
}
