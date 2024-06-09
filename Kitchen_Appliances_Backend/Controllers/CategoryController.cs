using AutoMapper;
using Kitchen_Appliances_Backend.DTO.Category;
using Kitchen_Appliances_Backend.DTO.Role;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Repositores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

		public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories() 
        {
            return Ok(await _categoryRepository.GetAllCategories());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var result = await _categoryRepository.GetCategoryById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _categoryRepository.CreateCategory(request)); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return Ok(await _categoryRepository.DeleteCategory(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id,UpdateCategoryRequest request)
        {
            return Ok(await _categoryRepository.UpdateCategory(id, request));
        }
    }
}
