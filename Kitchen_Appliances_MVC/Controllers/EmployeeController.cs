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
        private readonly IVNPayClientService test;

        public EmployeeController(IVNPayClientService cartDetailServiceClient)
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
            var res = await test.CreatePaymentUrl();
            return Redirect(res.Data);
        }

        //ContentResult , ViewResult, JsonResult
        public async Task<IActionResult> Detail(int id)
        {
            //var res = await test.GetBillInformation(id);
            //var a = res.Data;
            //Console.WriteLine("Mã hóa đơn : " + a.OrderId + " Total: " + a.Total);
            return View();
        }

        public IActionResult CreatePayment()
        {
            string res = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount=2000000&vnp_Command=pay&vnp_CreateDate=20240609163212&vnp_CurrCode=VND&vnp_IpAddr=172.27.128.1&vnp_Locale=vn&vnp_OrderInfo=Tr%E1%BA%A7n+H%E1%BB%AFu+Chi%E1%BA%BFn+Thanh+to%C3%A1n+VNPAY+20000&vnp_OrderType=orther&vnp_TmnCode=CJE84SL7&vnp_TxnRef=1_638535474921358212&vnp_Version=2.1.0&vnp_SecureHash=9214e91f0b54a4d225784e819479e80f19a7786cc756ca810f427aa83d1764e0ed17d96553ab3d9335d949184a3b5183a0d393712e888201d6a6a5397a4108b6";
            return Redirect(res);
        }

    }
}
