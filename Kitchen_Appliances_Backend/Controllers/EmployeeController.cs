using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Enums;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.DTO.Employee;
using Kitchen_Appliances_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost("active-account")]
        public async Task<IActionResult> ActiveAccount(VerifyOTPRequest request)
        {
            request.Type = TOKEN_TYPE.REGISTER_OTP;
            return Ok(await _repo.ActiveAccount(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAll()
        {
            var employees = _mapper.Map<List<EmployeeDTO>>(_repo.ListEmployee());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromQuery] CreateEmployeeRequest request)
        {
            var res = await _repo.CreateEmployee(request);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            var employee = await _repo.GetEmployeeById(id);
            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            return Ok(new ApiResponse<EmployeeDTO>(status: 200, message : "Success", employeeDTO));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, [FromQuery] UpdateEmployeeRequest request)
        {
            return Ok(await _repo.UpdateEmployee(id, request));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            return Ok(await _repo.DeleteEmployee(id));
        }
    }
}
