using AutoMapper;
using Kitchen_Appliances_Backend.DTO.Image;
using Kitchen_Appliances_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ImageController(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetListImage([FromRoute] int productId) 
        {
            var res =await _imageRepository.GetAllImages(productId);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageById([FromRoute] int id)
        {
            var res = await _imageRepository.GetImageById(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateImage(CreateImageRequest request)
        {
            var res = await _imageRepository.CreateImage(request);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImage([FromRoute] int id, UpdateImageRequest request)
        {
            var res = await _imageRepository.UpdateImage(id, request);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageById([FromRoute] int id)
        {
            var res = await _imageRepository.DeleteImage(id);
            return Ok(res);
        }
    }
}
