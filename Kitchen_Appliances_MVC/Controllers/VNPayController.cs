using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.Bill;
using Kitchen_Appliances_MVC.ViewModels.VNPAY;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_MVC.Controllers
{
    public class VNPayController : Controller
    {
        private readonly IVNPayClientService _vNPayClientService;
        private readonly IBillServiceClient _billServiceClient;
        public VNPayController(IVNPayClientService service)
        {
            _vNPayClientService = service;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> CreatePayment(int id)
        {
            var dataPayment = await _vNPayClientService.CreatePaymentUrl(id);
            if (dataPayment.Status != 200)
            {
                ViewBag.error = dataPayment.Message;
                return View("PaymentErrorView");
            }
            else
            {
                return Redirect(dataPayment.Data);
            }
        }

        //Không thể sử dụng được nữa 
        //public async Task<IActionResult> CallBackPayment()
        //{
        //    var model = await _vNPayClientService.LoadDataPaymentSuccess();
        //    if (model.Status == 200)
        //    {
        //        ViewBag.error = model.Message;
        //        return View(model.Data);
        //    }
        //    else
        //    {
        //        ViewBag.error = model.Message;
        //        return View(model);
        //    }
        //}
        public async Task<IActionResult> PrintBillPayment(int id)
        {
            var response = await _billServiceClient.GetBillInformation(id);
            if(response.Status == 200) 
            {
                BillDto bill = response.Data;
                return View(bill);
            }
            return View();
        }
    }
}
