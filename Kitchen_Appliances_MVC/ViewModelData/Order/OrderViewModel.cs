using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Customer;
using Kitchen_Appliances_MVC.ViewModels.Employee;
using Kitchen_Appliances_MVC.ViewModels.Order;

namespace Kitchen_Appliances_MVC.ViewModelData.Order
{
	public class OrderViewModel
	{
		public List<CategoryDTO> Categories { get; set; }
		public CustomerDTO Customer { get; set; }
		public List<OrderDTO> Orders { get; set; }
		public List<EmployeeDTO> Employees { get; set; }
	}
}
