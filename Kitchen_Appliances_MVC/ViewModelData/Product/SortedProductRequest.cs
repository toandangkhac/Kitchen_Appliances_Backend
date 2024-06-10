using Kitchen_Appliances_MVC.ViewModels.Product;

namespace Kitchen_Appliances_MVC.ViewModelData.Product
{
	public class SortedProductRequest
	{
		public string sortOption { get; set; }
		public List<ProductDTO> products { get; set; }
	}
}
