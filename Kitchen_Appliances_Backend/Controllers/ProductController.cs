using Kitchen_Appliances_Backend.DTO.Product;
using Kitchen_Appliances_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            return Ok(await _productRepository.GetProductById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts() 
        {
            return Ok(await _productRepository.GetAllProducts());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct( CreateProductRequest request)
        {
            return Ok( await _productRepository.CreateProduct(request));
        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateProduct([FromRoute] int id,UpdateProductRequest request)
        {
            return Ok(await _productRepository.UpdateProduct(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            return Ok(await _productRepository.DeleteProduct(id));
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> ListProductByCategory([FromRoute] int id)
        {
            return Ok(await _productRepository.ListProductByCategory(id));
        }
    }
}
