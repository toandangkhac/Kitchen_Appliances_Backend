using Kitchen_Appliances_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
	public class OrderDetailController : Controller
	{
		private readonly IOrderdetailRepository _orderdetailRepository;

		public OrderDetailController(IOrderdetailRepository orderdetailRepository)
		{
			_orderdetailRepository = orderdetailRepository;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("get-order-detail/{orderDetailId}")]
		public async Task<IActionResult> GetOrderDetailById(int orderDetailId)
		{
			var res = await _orderdetailRepository.GetOrderDetailById(orderDetailId);
			return Ok(res);
		}


		[HttpGet("list-order-detail/{orderId}")]
		public async Task<IActionResult> GetAllOrderDetailsByOrder(int orderId)
		{
			var res = await _orderdetailRepository.GetAllOrderDetailsByOrder(orderId);
			return Ok(res);
		}
	}
}
