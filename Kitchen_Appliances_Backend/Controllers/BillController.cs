using Kitchen_Appliances_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BillController : ControllerBase
	{
		private readonly IBillRepository _billRepository;
		public BillController(IBillRepository billRepository)
		{
			this._billRepository = billRepository;
		}

		[HttpGet("save/{orderId}")]
		public async Task<IActionResult> savePaymentInfor(int orderId)
		{
			var res = await _billRepository.savePaymentInfor(orderId);
			return Ok(res);
		}

		[HttpGet("get/{billId}")]
		public async Task<IActionResult> GetBillInformation(int billId)
		{
			var result = await _billRepository.GetBillInformation(billId);
			return Ok(result);
		}

		[HttpGet("list")]
		public async Task<IActionResult> GetAllBill()
		{
			return Ok(await _billRepository.GetAllBill());
		}
	}
}
