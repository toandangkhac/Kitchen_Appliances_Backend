using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Customer;

namespace Kitchen_Appliances_MVC.ViewModelData.Customer
{
	public class ChangeInfoViewModel
	{
		public CustomerDTO Customer { get; set; }
		public List<CategoryDTO> Categories { get; set; }
	}
}
