using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ApiServices;
using Kitchen_Appliances_MVC.Services;
using Kitchen_Appliances_MVC.ViewModelData.Admin;
using Kitchen_Appliances_MVC.ViewModelData.Customer;
using Kitchen_Appliances_MVC.ViewModelData.Image;
using Kitchen_Appliances_MVC.ViewModelData.Order;
using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Customer;
using Kitchen_Appliances_MVC.ViewModels.Employee;
using Kitchen_Appliances_MVC.ViewModels.Image;
using Kitchen_Appliances_MVC.ViewModels.Order;
using Kitchen_Appliances_MVC.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_MVC.Controllers
{
	public class AdminController : Controller
	{
		private readonly IProductServiceClient _productServiceClient;
		private readonly ICategoryServiceClient _categoryServiceClient;
		private readonly ICustomerServiceClient _customerServiceClient;
		private readonly IOrderServiceClient _orderServiceClient;
		private readonly IEmployeeClient _employeeServiceClient;
		private readonly IImageServiceClient _imageServiceClient;
		private readonly IUploadService _upload;
		public AdminController(IProductServiceClient productServiceClient, ICategoryServiceClient categoryServiceClient,
			ICustomerServiceClient customerServiceClient, IOrderServiceClient orderServiceClient, IEmployeeClient employeeServiceClient, 
			IImageServiceClient imageServiceClient, IUploadService uploadService)
		{
			_upload = uploadService;
			_productServiceClient = productServiceClient;
			_categoryServiceClient = categoryServiceClient;
			_customerServiceClient = customerServiceClient;
			_orderServiceClient = orderServiceClient;
			_employeeServiceClient = employeeServiceClient;
			_imageServiceClient = imageServiceClient;
		}
		public IActionResult Index()
		{
			return View();
		}
		//product
		public async Task<IActionResult> ManageProduct()
		{
			var dataProducts = await _productServiceClient.GetAllProducts();
			if(dataProducts.Status != 200) {
				Console.WriteLine(dataProducts.Message);
			}
			ManageProductViewModel Model = new ManageProductViewModel(){
				Products = dataProducts.Data
			};
			return View(Model);
		}
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
			var dataCategories = await _categoryServiceClient.GetAllCategories();
			if(dataCategories.Status != 200)
			{
				Console.WriteLine(dataCategories.Message);
			}
			List<CategoryDTO> categories = dataCategories.Data;
            var modelView = new AddProductViewModel()
            {
                Categories = categories
            };
            return View(modelView);
        }
		[HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
			var checkCreate = await _productServiceClient.CreateProduct(request);
            Console.WriteLine(request.Name + "|" + request.Quantity + "|" + request.Price + "|" + request.Description + "|" + request.CategoryId);
            if (checkCreate.Status != 200)
			{
				Console.WriteLine(checkCreate);
			}
            return RedirectToAction("Index", "Admin");
        }
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {

            var dataProduct = await _productServiceClient.GetProductById(id);
			if(dataProduct.Status != 200)
			{
				Console.WriteLine(dataProduct.Message);
			}
			ProductDTO product = dataProduct.Data;
            var viewModel = new EditProductViewModel()
            {
                Product = product
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, UpdateProductRequest request)
        {
            var checkUpdate = await _productServiceClient.UpdateProduct(id, request);
			if(checkUpdate.Status != 200)
			{
				Console.WriteLine(checkUpdate.Message);
			}
            return RedirectToAction("ManageProduct", "Admin");
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var checkDelete = await _productServiceClient.DeleteProduct(id);
			if(checkDelete.Status != 200)
			{
                Console.WriteLine(checkDelete.Message);
            }
            return RedirectToAction("ManageProduct", "Admin");
        }
        //category
        public async Task<IActionResult> ManageCategory()
		{
			var dataCategories = await _categoryServiceClient.GetAllCategories();
			if (dataCategories.Status != 200)
			{
				Console.WriteLine(dataCategories.Message);
			}
			List<CategoryDTO> categories = dataCategories.Data;
			ManageCategoryViewModel Model = new ManageCategoryViewModel()
			{
				Categories = categories
			};
			return View(Model);
		}
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var checkCreate = await _categoryServiceClient.CreateCategory(request);
            if (checkCreate.Status != 200)
            {
                Console.WriteLine(checkCreate);
            }
            return RedirectToAction("ManageCategory", "Admin");
        }
        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var dataCategory = await _categoryServiceClient.GetCategoryById(id);
            if (dataCategory.Status != 200)
            {
                Console.WriteLine(dataCategory.Message);
            }
            CategoryDTO category = dataCategory.Data;
            var categoryModel = new EditCategoryViewModel()
            {
                Category = category,
            };
            return View(categoryModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(int id, UpdateCategoryRequest request)
        {
            var checkUpdate = await _categoryServiceClient.UpdateCategory(id, request);
            if (checkUpdate.Status != 200)
            {
                Console.WriteLine(checkUpdate.Message);
            }
            return RedirectToAction("ManageCategory", "Admin");
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var products = await _productServiceClient.ListProductByCategory(id);
			if (products.Status != 200)
			{
				Console.WriteLine(products.Message);
			}
            if (products.Data.Count==0)
            {
                return RedirectToAction("ManageCategory", "Admin");
            }
            var checkDelete = await _categoryServiceClient.DeleteCategory(id);
            if (checkDelete.Status != 200)
            {
                Console.WriteLine(checkDelete.Message);
            }
            return RedirectToAction("ManageCategory", "Admin");
        }
        //Customer
        public async Task<IActionResult> ManageCustomer()
		{
			var dataCustomer = await _customerServiceClient.ListCustomer();
			if (dataCustomer.Status != 200)
			{
				Console.WriteLine(dataCustomer.Message);
			}
			List<CustomerDTO> customers = dataCustomer.Data;
			ManageCustomerViewModel Model = new ManageCustomerViewModel()
			{
				CustomerDTOs = customers
			};
			return View(Model);
		}
		//Order
		public async Task<IActionResult> ManageOrder()
		{
			var dataorderDtos = await _orderServiceClient.ListOrderNotConfirm();
			if(dataorderDtos.Status != 200)
			{
				Console.WriteLine(dataorderDtos.Message);
			}
			List<OrderDTO> orders = dataorderDtos.Data;
			var dataCustomer = await _customerServiceClient.ListCustomer();
			if (dataCustomer.Status != 200)
			{
				Console.WriteLine(dataCustomer.Message);
			}
			List<CustomerDTO> customers = dataCustomer.Data;
			var dataEmployee = await _employeeServiceClient.GetListAll();
			if (dataEmployee.Status != 200)
			{
				Console.WriteLine(dataEmployee.Message);
			}
			List<EmployeeDTO> employees = dataEmployee.Data;
			var viewModel = new ManageOrderViewModel()
			{
				Orders = orders,
				Customer = customers,
				Employees = employees
			};
			return View(viewModel);
		}

		//Image
		[HttpGet]
		public async Task<IActionResult> ManageImage()
		{ 
			var dataProducts = await _productServiceClient.GetAllProducts();
			if (dataProducts.Status != 200)
			{
				Console.WriteLine(dataProducts.Message);
			}
			AddImageViewModel Model = new AddImageViewModel()
			{
				Products = dataProducts.Data,
			};
			return View(Model);
		}
		[HttpPost]
		public async Task<IActionResult> ManageImage(CreateImage request)
		{
			if (!ModelState.IsValid)
			{
				return View(request);
			}
			var dataproduct = await _productServiceClient.GetProductById(request.ProductId);
			if (dataproduct.Status != 200)
			{
				Console.WriteLine(dataproduct.Message);
			}
			
			var requestImage = new CreateImageRequest()
			{
				ProductId = request.ProductId,
			};
			if (request.Url != null)
			{
				requestImage.Url = await _upload.UploadFile(request.Url);
			}

			var checkCreate = await _imageServiceClient.CreateImage(requestImage);
			if (checkCreate.Status != 200)
			{
				Console.WriteLine(checkCreate.Message);
			}
			if (checkCreate.Data)
			{
				Console.WriteLine("Thêm ảnh thành công");
			}
			else
			{
				Console.WriteLine("Thêm ảnh thất bại");
			}
			return RedirectToAction("Index", "Admin");
		}
	}
}
