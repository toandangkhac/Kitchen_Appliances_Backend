using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Bill;
using Kitchen_Appliances_Backend.PaymentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IVnPayService _vpnPayService;
        private readonly DataContext _context;
        public VNPayController(IVnPayService vpnPayService, DataContext context)
        {
            _vpnPayService = vpnPayService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> CreatePaymentUrl()
        {
            //var order = _context.Orders.Find(request.OrderId);
            var model = new VnPaymentRequestModel()
            {
                OrderId = "1",
                Description = "Thanh toán VNPAY",
                Amount = 20000,
                FullName = "Trần Hữu Chiến"
            };
            var data = _vpnPayService.CreatePaymentUrl(HttpContext, model);

            var response = new ApiResponse<string>()
            {
                Status = 200,
                Message = "Lấy thông tin thanh toán",
                Data = data
            };
            return Ok(response);
        }

    }
}
