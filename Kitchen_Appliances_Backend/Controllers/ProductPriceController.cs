using Kitchen_Appliances_Backend.DTO.ProductPrice;
using Kitchen_Appliances_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPriceController : ControllerBase
    {
        private readonly IProductPriceRepository _productPriceRepository;

        public ProductPriceController(IProductPriceRepository productPriceRepository)
        {
            _productPriceRepository = productPriceRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetListProductPrice([FromRoute] int id) 
        {
            return Ok(await _productPriceRepository.ListProductImageByProduct(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductPrice(UpdateProductPriceRequest request)
        {
            return Ok(await _productPriceRepository.UpdateProductPrice(request));
        }
    }
}
