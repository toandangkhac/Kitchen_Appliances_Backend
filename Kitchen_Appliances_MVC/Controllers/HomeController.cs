using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.Models;
using Kitchen_Appliances_MVC.ViewModelData.Header;
using Kitchen_Appliances_MVC.ViewModelData.Home;
using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Image;
using Kitchen_Appliances_MVC.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kitchen_Appliances_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryServiceClient _categoryServiceClient;
        private readonly IProductServiceClient _productServiceClient;
        private readonly IImageServiceClient _imageServiceClient;

        public HomeController(ILogger<HomeController> logger, ICategoryServiceClient categoryServiceClient,
            IProductServiceClient productServiceClient, IImageServiceClient imageServiceClient)
        {
            _productServiceClient = productServiceClient;
            _logger = logger;
            _categoryServiceClient = categoryServiceClient;
            _imageServiceClient = imageServiceClient;
        }

        public async Task<IActionResult> Index()
        {
            var dataCategories = await _categoryServiceClient.GetAllCategories();
            if(dataCategories.Status != 200)
            {
                Console.WriteLine(dataCategories.Message);
            }
            List<CategoryDTO> categories = dataCategories.Data;

            var dataProducts = await _productServiceClient.GetAllProducts();
            if(dataProducts.Status != 200)
            { Console.WriteLine(dataProducts.Message);
            }
            List<ProductDTO> products = dataProducts.Data;
			List<ImageDTO> images = new List<ImageDTO>();
			foreach (ProductDTO product in products)
			{
                var dataImages = await _imageServiceClient.GetAllImagesByProduct(product.Id);
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
			HomeViewModels Models = new HomeViewModels
			{
				Categories = categories,
				Products = products,
				Images = images
			};

			return View(Models);
		}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
