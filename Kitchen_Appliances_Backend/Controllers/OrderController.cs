using Kitchen_Appliances_Backend.DTO.Order;
using Kitchen_Appliances_Backend.DTO.OrderDetail;
using Kitchen_Appliances_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("get-order-by-customer/{id}")]
        public async Task<IActionResult> ListOrderByCustomer(int id)
        {
            return Ok( await _orderRepository.ListOrderByCustomer(id));
        }
        [HttpPost("create-order-by-customer")]
        public async Task<IActionResult> createOrder(CreateOrderRequest request)
        {
            // get list order details
            var res = await _orderRepository.CreateOrder(request);
            return Ok(res);
        }
        [HttpPut("confirm-order-by-employee")]
        public async Task<IActionResult> confirmOrder(ConfirmOrderRequest request)
        {
            var res = await _orderRepository.ConfirmOrder(request);
            return Ok(res);
        }

        [HttpPut("cancel-order/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var res = await _orderRepository.CancelOrder(orderId);
            return Ok(res);
        }

		[HttpPut("confirm-delivery-success/{orderId}")]
		public async Task<IActionResult> ConfirmOrderDeliverySucess(int orderId)
		{
			var res = await _orderRepository.ConfirmOrderDeliverySucess(orderId);
			return Ok(res);
		}

  //      [HttpGet("list-order-confirm")]
  //      public async Task<IActionResult> ListOrderConfirmed()
  //      {
  //          var res = await _orderRepository.ListOrderConfirmed();
  //          return Ok(res);
  //      }

		//[HttpGet("list-order-not-confirm")]
		//public async Task<IActionResult> ListOrderNotConfirm()
		//{
		//	var res = await _orderRepository.ListOrderNotConfirm();
		//	return Ok(res);
		//}

        [HttpGet("list-all-order")]
        public async Task<IActionResult> ListAllOrders()
        {
            return Ok(await _orderRepository.ListAllOrders());
        }

        [HttpGet("thanh-toan-khi-nhan-hang/{orderId}")]
        public async Task<IActionResult> ThanhToanKhiNhanHang(int orderId)
        {
            return Ok(await _orderRepository.ThanhToanKhiNhanHang(orderId));
        }
	}
}
