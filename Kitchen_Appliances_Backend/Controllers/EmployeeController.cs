using AutoMapper;
using Kitchen_Appliances_Backend.DTO;
using Kitchen_Appliances_Backend.DTO.Employee;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Repositores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _mapper.Map<List<EmployeeDTO>>(_repo.ListEmployee());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(employees);
        }

    }
}
