using Kitchen_Appliances_MVC.ViewModels.Customer;
using Kitchen_Appliances_MVC.ViewModels.Employee;
using Kitchen_Appliances_MVC.ViewModels.Order;

namespace Kitchen_Appliances_MVC.ViewModelData.Order
{
	public class ManageOrderViewModel
	{
		public List<CustomerDTO> Customer { get; set; }
		public List<OrderDTO> Orders { get; set; }
		public List<OrderDTO> OrdersConfirm { get; set; }
		public List<EmployeeDTO> Employees { get; set; }
	}
}
