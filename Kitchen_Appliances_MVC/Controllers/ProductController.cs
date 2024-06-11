using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModelData.Header;
using Kitchen_Appliances_MVC.ViewModelData.Product;
using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Image;
using Kitchen_Appliances_MVC.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;

namespace Kitchen_Appliances_MVC.Controllers
{
	public class ProductController : Controller
	{
		private readonly ICategoryServiceClient _categoryServiceClient;
		private readonly IProductServiceClient _productServiceClient;
		private readonly IImageServiceClient _imageServiceClient;
		public ProductController(ICategoryServiceClient categoryServiceClient, IProductServiceClient productServiceClient, IImageServiceClient imageServiceClient)
		{
			_categoryServiceClient = categoryServiceClient;
			_productServiceClient = productServiceClient;
			_imageServiceClient = imageServiceClient;
		}

		public async Task<IActionResult> Index(string input, string option)
		{
			var dataCategories = await _categoryServiceClient.GetAllCategories();
			if (dataCategories.Status != 200)
			{
				Console.WriteLine(dataCategories.Message);
			}
			List<CategoryDTO> categories = dataCategories.Data;
			Console.WriteLine(categories.Count);
			List<ProductDTO> products = new List<ProductDTO>();
			List<ProductDTO> productsAZ = new List<ProductDTO>();
			if (!option.Equals("All"))
			{
				int CateId = categories.FirstOrDefault(c => c.Name == option).Id;
				var dataProducts = await _productServiceClient.ListProductByCategory(CateId);
				if (dataProducts.Status != 200)
				{
					Console.WriteLine(dataProducts.Message);
				}
				products = dataProducts.Data;
			}
			else
			{
				var dataProducts = await _productServiceClient.GetAllProducts();
				if (dataProducts.Status != 200)
				{
					Console.WriteLine(dataProducts.Message);
				}
				products = dataProducts.Data;
			}
			if (input == null || input.Trim().Equals(""))
			{
				productsAZ = products;
			}
			else
			{
				productsAZ = products.Where(p => p.Name.Contains(input.Trim())).ToList();
			}
			Dictionary<int, string> images = new Dictionary<int, string>(); // idProduct, ImageURL
			var headerViewModel = new HeaderViewModel()
			{
				Categories = categories
			};
			ViewBag.HeaderData = headerViewModel;
			SearchProductViewModel Model = new SearchProductViewModel()
			{
				Categories = categories,
				Products = productsAZ,
				Images = await GetProductImages(productsAZ)
			};
			return View(Model);
		}
		[HttpPost]
		public async Task<IActionResult> GetSortedProducts([FromBody] SortedProductRequest request)
		{
			var dataCategories = await _categoryServiceClient.GetAllCategories();
			if (dataCategories.Status != 200)
			{
				Console.WriteLine(dataCategories.Message);
			}
			List<CategoryDTO> categories = dataCategories.Data;
			var pros = request.products;
			var listpr = GetProductsSorted(request);
			var model = new SearchProductViewModel
			{
				Categories = categories,
				Products = listpr,
				Images = await GetProductImages(listpr)
			};

			// Trả về partial view với danh sách sản phẩm đã sắp xếp
			return PartialView("_ProductList", model);
		}
		private List<ProductDTO> GetProductsSorted(SortedProductRequest request)
		{
			var products = request.products;
			switch (request.sortOption)
			{
				case "az":
					return products;
				case "za":
					products.Reverse();
					return products;
				case "stock":
					return products.OrderByDescending(p => p.Quantity).ToList();
				case "price":
					return products.OrderBy(p => p.Price).ToList();
				default:
					return products;
			}
		}
		private async Task<Dictionary<int, string>> GetProductImages(List<ProductDTO> products)
		{
			Dictionary<int, string> images = new Dictionary<int, string>(); // idProduct, ImageURL
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
			return images;
		}
		public async Task<IActionResult> ProductDetail(int id)
		{
			var dataCategories = await _categoryServiceClient.GetAllCategories();
			if (dataCategories.Status != 200)
			{
				Console.WriteLine(dataCategories.Message);
			}
			List<CategoryDTO> categories = dataCategories.Data;
			var dataProduct = await _productServiceClient.GetProductById(id);
			if(dataProduct.Status != 200) {
				Console.WriteLine(dataProduct.Message);
			}
			ProductDTO product = dataProduct.Data;
			var dataProducts = await _productServiceClient.ListProductByCategory(product.CategoryId);
			if (dataProducts.Status != 200)
			{
				Console.WriteLine(dataProducts.Message);
			}
			List<ProductDTO> products = dataProducts.Data;
			var dataImgforPr = await _imageServiceClient.GetAllImagesByProduct(id);
			if(dataImgforPr.Status != 200)
			{
				Console.WriteLine(dataImgforPr.Message);
			}
			List<ImageDTO> imageforProduct = dataImgforPr.Data;
			var img = new ImageDTO()
			{
				Id = 0,
				ProductId = id,
				Url = "https://png.pngtree.com/background/20210715/original/pngtree-white-border-texture-textured-background-picture-image_1290377.jpg"
			};
			if (imageforProduct.Count == 0)
			{
				imageforProduct.Add(img);
				imageforProduct.Add(img);
				imageforProduct.Add(img);
			}
			List <ImageDTO> images = new List<ImageDTO>();
			foreach (ProductDTO prd in products)
			{
				var dataImages = await _imageServiceClient.GetAllImagesByProduct(prd.Id);
				List<ImageDTO> imagesTemp = dataImages.Data;
				//if (imagesTemp != null && imagesTemp.Count > 0)
				//	images.Add(imagesTemp[0]);
				if (imagesTemp != null && imagesTemp.Count > 0)
				{
					images.Add(imagesTemp[0]);
				}
				else
				{
					var Image = new ImageDTO()
					{
						Id = 0,
						ProductId = product.Id,
						Url = "https://png.pngtree.com/background/20210715/original/pngtree-white-border-texture-textured-background-picture-image_1290377.jpg"
					};
					images.Add(Image);
				}
			}
			var headerViewModel = new HeaderViewModel()
			{
				Categories = categories
			};
			ViewBag.HeaderData = headerViewModel;
			ProductViewModel Model = new ProductViewModel()
			{
				Categories = categories,
				ImagesForProduct = imageforProduct,
				Product = product,
				Products = products,
				Images = images
			};
			return View(Model);
		}
	}
}
