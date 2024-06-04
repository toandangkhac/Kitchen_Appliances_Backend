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
    }
}
