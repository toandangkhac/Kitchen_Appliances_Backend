using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAccountRepository _account;

        public AuthController(IAccountRepository account)
        {
            _account = account;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromQuery] LoginAuthRequest request)
        {
            var authResp = await _account.Authenticate(request);

            return Ok(new ApiResponse<AuthDTO>(StatusCodes.Status200OK,"Login success", authResp));
        }



    }
}
