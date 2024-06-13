using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.CartDetail;
using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Product;
using Kitchen_Appliances_MVC.ViewModels.ProductPrice;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IBillServiceClient _billServiceClient;
        public EmployeeController(IBillServiceClient billServiceClient)
        {
            _billServiceClient = billServiceClient;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        //ContentResult , ViewResult, JsonResult
        public async Task<IActionResult> Detail(int id)
        {
            var response = await _billServiceClient.GetBillInformation(id);
            Console.WriteLine(response.Data.CustomerName);
            return View();
        }

    }
}
