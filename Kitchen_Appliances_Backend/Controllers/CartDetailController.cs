using Kitchen_Appliances_Backend.DTO.CartItem;
using Kitchen_Appliances_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {

        private readonly ICartDetailRepository _cartDetailRepository;

        public CartDetailController(ICartDetailRepository cartDetailRepository)
        {
            _cartDetailRepository = cartDetailRepository;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetListCartDetail([FromRoute] int customerId)
        {
            var carts = await _cartDetailRepository.GetCartDetailByCustomer(customerId);
            return Ok(carts);
        }

        [HttpPost]
        public async Task<IActionResult> AddCartDetailToCart(CreateCartDetailRequest request)
        {
            return Ok(await _cartDetailRepository.AddCartDetailToCart(request));
        }

        [HttpPost("find-by-id")]
        public async Task<IActionResult> GetCartItem(GetCartDetailRequest request)
        {
            return Ok(await _cartDetailRepository.GetCartDetail(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCartDetail(GetCartDetailRequest request)
        {
            return Ok(await _cartDetailRepository.DeleteCartDetail(request));
        }
    }
}
