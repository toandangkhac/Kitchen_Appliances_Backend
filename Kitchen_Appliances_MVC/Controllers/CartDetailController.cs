using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModelData.CartDetail;
using Kitchen_Appliances_MVC.ViewModelData.Header;
using Kitchen_Appliances_MVC.ViewModels.CartDetail;
using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Customer;
using Kitchen_Appliances_MVC.ViewModels.Image;
using Kitchen_Appliances_MVC.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_MVC.Controllers
{
	public class CartDetailController : Controller
	{
		private readonly ICartDetailServiceClient _cartDetailServiceClient;
		private readonly ICustomerServiceClient _customerServiceClient;
		private readonly IOrderServiceClient _orderServiceClient;
		private readonly IProductServiceClient _productServiceClient;
		private readonly ICategoryServiceClient _categoryServiceClient;
		private readonly IImageServiceClient _imageServiceClient;
		public CartDetailController(ICartDetailServiceClient cartDetailServiceClient, ICustomerServiceClient customerServiceClient, IOrderServiceClient orderServiceClient, IProductServiceClient productServiceClient, ICategoryServiceClient categoryServiceClient, IImageServiceClient imageServiceClient)
		{
			_cartDetailServiceClient = cartDetailServiceClient;
			_customerServiceClient = customerServiceClient;
			_orderServiceClient = orderServiceClient;
			_productServiceClient = productServiceClient;
			_categoryServiceClient = categoryServiceClient;
			_imageServiceClient = imageServiceClient;
		}

		public async Task<IActionResult> Index(string id)
		{
			int Idcustomer = int.Parse(id);
			var dataCategories = await _categoryServiceClient.GetAllCategories();
			if (dataCategories.Status != 200)
			{
				Console.WriteLine(dataCategories.Message);
			}
			List<CategoryDTO> categories = dataCategories.Data;
			var dataCustomer = await _customerServiceClient.GetCustomerById(Idcustomer);
			if (dataCustomer.Status != 200)
			{
				Console.WriteLine(dataCustomer.Message);
			}
			CustomerDTO customer = dataCustomer.Data;
			var dataCartDetails = await _cartDetailServiceClient.GetCartDetailByCustomer(Idcustomer);
			if (dataCartDetails.Status != 200)
			{
				Console.WriteLine(dataCartDetails.Message);
			}
			List<CartDetailDTO> cartDetails = dataCartDetails.Data;
			List<ProductDTO> products = new List<ProductDTO>();
			Dictionary<int, string> images = new Dictionary<int, string>(); // idProduct, ImageURL
			foreach (CartDetailDTO cartDetail in cartDetails)
			{
				var dataProductTemp = await _productServiceClient.GetProductById(cartDetail.ProductId);
				if (dataProductTemp.Status != 200)
				{
					Console.WriteLine(dataProductTemp.Message);
				}
				ProductDTO pr = dataProductTemp.Data;
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
			CartDetailViewModel Model = new CartDetailViewModel()
			{
				Categories = categories,
				Customer = customer,
				CartDetails = cartDetails,
				Products = products,
				Images = images
			};
			return View(Model);
		}
		public async Task<IActionResult> DeleteCartDetail(int ProductId, int CustomerId)
		{
			int count = int.Parse(HttpContext.Session.GetString("Cartcount"));
			var getCartDetailRequest = new GetCartDetailRequest()
			{
				ProductId = ProductId,
				CustomerId = CustomerId
			};
			var checkDetele = await _cartDetailServiceClient.DeleteCartDetail(getCartDetailRequest);
			if (checkDetele.Status != 200)
			{
				Console.WriteLine(checkDetele.Message);
			}
			else
			{
				count--;
				HttpContext.Session.SetString("Cartcount", count.ToString());
			}
			return Json(new
			{
				count
			});
		}
		[HttpPost]
		public async Task<IActionResult> AddToCart(int ProductId, int CustomerId, int Quantity)
		{
			int Count = 0;
			var createCartDetailRequest = new CreateCartDetailRequest()
			{
				ProductId = ProductId,
				CustomerId = CustomerId,
				Quantity = Quantity
			};
			var checkCreate = await _cartDetailServiceClient.AddCartDetailToCart(createCartDetailRequest);
			if (checkCreate.Status != 200)
			{
				Console.WriteLine(checkCreate.Message);
			}
			var getcartDetailByCustomerID = await _cartDetailServiceClient.GetCartDetailByCustomer(CustomerId);
			if (getcartDetailByCustomerID.Status != 200)
			{
				Console.WriteLine(getcartDetailByCustomerID.Message);
			}
			else
			{
				List<CartDetailDTO> cartDetails = getcartDetailByCustomerID.Data;
				Count = cartDetails.Count;
			}
			HttpContext.Session.SetString("Cartcount", Count.ToString());
			return Json(new
			{
				Count
			});
		}
	}
}
