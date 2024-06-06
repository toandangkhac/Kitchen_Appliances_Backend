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
        private readonly ICartDetailRepository _cartDetailRepository;

        public OrderController(IOrderRepository orderRepository, ICartDetailRepository cartDetailRepository)
        {
            _orderRepository = orderRepository;
            _cartDetailRepository = cartDetailRepository;
        }

        [HttpGet("get-order-by-customer/{id}")]
        public async Task<IActionResult> ListOrderByCustomer(int id)
        {
            return Ok( await _orderRepository.ListOrderByCustomer(id));
        }
        [HttpPost("create-order-by-customer/{id}")]
        public async Task<IActionResult> createOrder(int id)
        {
            
            
            // get list order details
            var res = await _orderRepository.CreateOrder(id);


            return Ok(res);
        }
        [HttpPut("confirm-order-by-employee/{employeeId}/{orderId}")]
        public async Task<IActionResult> confirmOrder(int employeeId, int orderId)
        {
            var res = await _orderRepository.ConfirmOrder(employeeId, orderId);
            return Ok(res);
        }
        
    }
}
