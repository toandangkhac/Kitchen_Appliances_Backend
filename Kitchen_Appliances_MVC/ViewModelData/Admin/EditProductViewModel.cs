using Kitchen_Appliances_MVC.ViewModels.Product;

namespace Kitchen_Appliances_MVC.ViewModelData.Admin
{
	public class EditProductViewModel
	{
		public ProductDTO Product { get; set; }

		public UpdateProductRequest request { get; set; }
	}
}
