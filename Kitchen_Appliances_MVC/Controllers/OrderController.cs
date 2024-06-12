using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ApiServices;
using Kitchen_Appliances_MVC.ViewModelData.Header;
using Kitchen_Appliances_MVC.ViewModelData.Order;
using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Customer;
using Kitchen_Appliances_MVC.ViewModels.Employee;
using Kitchen_Appliances_MVC.ViewModels.Image;
using Kitchen_Appliances_MVC.ViewModels.Order;
using Kitchen_Appliances_MVC.ViewModels.OrderDetail;
using Kitchen_Appliances_MVC.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Kitchen_Appliances_MVC.Controllers
{
	public class OrderController : Controller
	{
		private readonly ICartDetailServiceClient _cartDetailServiceClient;
		private readonly IOrderServiceClient _orderServiceClient;
		private readonly IOrderDetailServiceClient _orderDetailServiceClient;
		private readonly IProductServiceClient _productServiceClient;
		private readonly ICustomerServiceClient _customerServiceClient;
		private readonly ICategoryServiceClient _categoryServiceClient;
		private readonly IEmployeeClient _employeeClient;
		private readonly IImageServiceClient _imageServiceClient;
		public OrderController(ICartDetailServiceClient cartDetailServiceClient, IOrderServiceClient orderServiceClient
			, IOrderDetailServiceClient orderDetailServiceClient, IProductServiceClient productServiceClient
			, ICustomerServiceClient customerServiceClient, ICategoryServiceClient categoryServiceClient
			, IEmployeeClient employeeClient, IImageServiceClient imageServiceClient)
		{
			_cartDetailServiceClient = cartDetailServiceClient;
			_orderServiceClient = orderServiceClient;
			_orderDetailServiceClient = orderDetailServiceClient;
			_productServiceClient = productServiceClient;
			_customerServiceClient = customerServiceClient;
			_categoryServiceClient = categoryServiceClient;
			_employeeClient = employeeClient;
			_imageServiceClient = imageServiceClient;
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
			var dataEmployees = await _employeeClient.GetListAll();
			if (dataEmployees.Status != 200)
			{
				Console.WriteLine(dataEmployees.Message);
			}
			List<EmployeeDTO> employees = dataEmployees.Data;
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
				Employees = employees
			};
			return View(Model);
		}
		public async Task<IActionResult> ChangeOrder(int id, int status, string userid)
		{
			int idu = int.Parse(userid);
			if(status == 1)
			{
				Console.WriteLine(id);
				var checkCancel = await _orderServiceClient.CancelOrder(id);
				if(checkCancel.Status != 200)
				{
					Console.WriteLine(checkCancel.Message);
				}
				//return Json(new { id, status });
			}
			else
			{
				var checkConfirm = await _orderServiceClient.ConfirmOrderDeliverySucess(id);
				if (checkConfirm.Status != 200)
				{
					Console.WriteLine(checkConfirm.Message);
				}
				//return Json(new { id, status });
			}
			return RedirectToAction("Index",new { id = idu });
		}
		public async Task<IActionResult> OrderDetail(int id, string idcustomer) // id order, id Customer
		{
			int idCustomer = int.Parse(idcustomer);
			var dataCategories = await _categoryServiceClient.GetAllCategories();
			if (dataCategories.Status != 200)
			{
				Console.WriteLine(dataCategories.Message);
			}
			List<CategoryDTO> categories = dataCategories.Data;
			var dataCustomer = await _customerServiceClient.GetCustomerById(idCustomer);
			if (dataCustomer.Status != 200)
			{
				Console.WriteLine(dataCustomer.Message);
			}
			CustomerDTO customer = dataCustomer.Data;
			var dataOrderDetails = await _orderDetailServiceClient.GetAllOrderDetailsByOrder(id);
			if(dataOrderDetails.Status != 200)
			{
				Console.WriteLine(dataOrderDetails.Message);
			}
			List<OrderDetailDTO> orderDetails = dataOrderDetails.Data;
			List<ProductDTO> products = new List<ProductDTO>();
			Dictionary<int, string> images = new Dictionary<int, string>(); // idProduct, ImageURL
			foreach (OrderDetailDTO orderDetail in orderDetails)
			{
				var dataProduct = await _productServiceClient.GetProductById(orderDetail.ProductId);
				if (dataProduct.Status != 200)
				{
					Console.WriteLine(dataProduct.Message);
				}
				ProductDTO pr = dataProduct.Data;
				products.Add(pr);
			}
			foreach (ProductDTO prd in products)
			{
				var dataImages = await _imageServiceClient.GetAllImagesByProduct(prd.Id);
				if (dataImages.Status != 200)
				{
					Console.WriteLine(dataImages.Message);
				}
				List<ImageDTO> imagesTemp = dataImages.Data;
				if (imagesTemp != null && imagesTemp.Count > 0)
				{
					images.Add(prd.Id, imagesTemp[0].Url);
				}
				else
				{
					images.Add(prd.Id, "https://png.pngtree.com/background/20210715/original/pngtree-white-border-texture-textured-background-picture-image_1290377.jpg");
				}
			}
			var headerViewModel = new HeaderViewModel()
			{
				Categories = categories
			};
			ViewBag.HeaderData = headerViewModel;
			OrderDetailViewModel Model = new OrderDetailViewModel()
			{
				Categories = categories,
				Customer = customer,
				//Order = order,
				OrderDetails = orderDetails,
				Products = products,
				Images = images
			};
			return View(Model);
		}
	}
}
