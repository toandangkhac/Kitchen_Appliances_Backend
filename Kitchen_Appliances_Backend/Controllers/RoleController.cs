using AutoMapper;
using Kitchen_Appliances_Backend.DTO;
using Kitchen_Appliances_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            this._roleRepository = roleRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var roles = _mapper.Map<List<RoleDTO>>(_roleRepository.GetRoles());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(roles);
        }
    }
}
