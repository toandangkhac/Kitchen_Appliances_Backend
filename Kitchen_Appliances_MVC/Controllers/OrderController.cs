using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModelData.Header;
using Kitchen_Appliances_MVC.ViewModelData.Order;
using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Customer;
using Kitchen_Appliances_MVC.ViewModels.Employee;
using Kitchen_Appliances_MVC.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Kitchen_Appliances_MVC.Controllers
{
	public class OrderController : Controller
	{
		private readonly ICartDetailServiceClient _cartDetailServiceClient;
		private readonly IOrderServiceClient _orderServiceClient;
		//private readonly IOrderDetailServiceClient _orderDetailServiceClient;
		private readonly IProductServiceClient _productServiceClient;
		private readonly ICustomerServiceClient _customerServiceClient;
		private readonly ICategoryServiceClient _categoryServiceClient;
		private readonly IEmployeeClient _employeeClient;
		public OrderController(ICartDetailServiceClient cartDetailServiceClient, IOrderServiceClient orderServiceClient, /*IOrderDetailServiceClient orderDetailServiceClient,*/ IProductServiceClient productServiceClient, ICustomerServiceClient customerServiceClient, ICategoryServiceClient categoryServiceClient, IEmployeeClient employeeClient)
		{
			_cartDetailServiceClient = cartDetailServiceClient;
			_orderServiceClient = orderServiceClient;
			//_orderDetailServiceClient = orderDetailServiceClient;
			_productServiceClient = productServiceClient;
			_customerServiceClient = customerServiceClient;
			_categoryServiceClient = categoryServiceClient;
			_employeeClient = employeeClient;
		}


		public async Task<IActionResult> Index(string id)
		{
			int IdCustomer = int.Parse(id);
			var dataCategories = await _categoryServiceClient.GetAllCategories();
			if (dataCategories.Status != 200)
			{
				Console.WriteLine(dataCategories.Message);
			}
			List<CategoryDTO> categories = dataCategories.Data;
			var dataCustomer = await _customerServiceClient.GetCustomerById(IdCustomer);
			if (dataCustomer.Status != 200)
			{
				Console.WriteLine(dataCustomer.Message);
			}
			CustomerDTO customer = dataCustomer.Data;
			var dataOrders = await _orderServiceClient.ListOrderByCustomer(IdCustomer);
			if (dataOrders.Status != 200)
			{
				Console.WriteLine(dataOrders.Message);
			}
			List<OrderDTO> orders = dataOrders.Data;
			//var dataEmployees = await _employeeClient.GetListAll();
			//if (dataEmployees.Status != 200)
			//{
			//	Console.WriteLine(dataEmployees.Message);
			//}
			//List<EmployeeDTO> employees = dataEmployees.Data;
			var headerViewModel = new HeaderViewModel()
			{
				Categories = categories
			};
			ViewBag.HeaderData = headerViewModel;
			OrderViewModel Model = new OrderViewModel()
			{
				Categories = categories,
				Customer = customer,
				Orders = orders,
				//Employees = employees
			};
			return View(Model);
		}
		//public async Task<IActionResult> OrderDetail(int id, string idcustomer) // id order, id Customer
		//{
		//	int idCustomer = int.Parse(idcustomer);
		//	var dataCategories = await _categoryServiceClient.GetAllCategories();
		//	if (dataCategories.Status != 200)
		//	{
		//		Console.WriteLine(dataCategories.Message);
		//	}
		//	List<CategoryDTO> categories = dataCategories.Data;
		//	var dataCustomer = await _customerServiceClient.GetCustomerById(idCustomer);
		//	if (dataCustomer.Status != 200)
		//	{
		//		Console.WriteLine(dataCustomer.Message);
		//	}
		//	CustomerDTO customer = dataCustomer.Data;
		//	//var dataOrderDetail = await _O
		//}
	}
}
