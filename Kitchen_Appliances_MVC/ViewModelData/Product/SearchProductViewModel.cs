using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Product;

namespace Kitchen_Appliances_MVC.ViewModelData.Product
{
	public class SearchProductViewModel
	{
		public List<CategoryDTO> Categories { get; set; }
		public List<ProductDTO> Products { get; set; }
		public Dictionary<int, string> Images { get; set; }
	}
}
