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
        private readonly IBillServiceClient test;

        public EmployeeController(IBillServiceClient cartDetailServiceClient)
        {
            test = cartDetailServiceClient;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Khi check luôn nhớ async await
            var cart = new GetCartDetailRequest()
            {
                CustomerId = 2,
                ProductId = 14,
            };
            var res = await test.GetAllBill();
            Console.WriteLine(res.Message);
            return View();
        }

        //ContentResult , ViewResult, JsonResult
        public async Task<IActionResult> Detail(int id)
        {
            var res = await test.GetBillInformation(id);
            var a = res.Data;
            Console.WriteLine("Mã hóa đơn : " + a.OrderId + " Total: " + a.Total);
            return View();
        }

    }
}
