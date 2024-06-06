using Kitchen_Appliances_Backend.DTO.Bill;
using Kitchen_Appliances_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : Controller
    {
        private readonly IBillRepository _billRepository;
        public BillController(IBillRepository billRepository)
        {
            this._billRepository = billRepository;
        }

        [HttpPost("save-payment-info")]
        public async Task<IActionResult> savePaymentInfor(CreateBillRequest billRequest)
        {
            var res = _billRepository.savePaymentInfor(billRequest);
            return Ok(res);
        }
    }
}
