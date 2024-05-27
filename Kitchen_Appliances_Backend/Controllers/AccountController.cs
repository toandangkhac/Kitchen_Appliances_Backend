using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public async Task<IActionResult> listAccount()
        {
            return Ok(await _accountRepository.listAccount());
        }

        [Authorize(Roles = "Quản trị viên")]
        [HttpGet("find-email/{email}")]
        public async Task<IActionResult> findAccount(string email) 
        {
            var res = await _accountRepository.findAccount(email);
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginAuthRequest request)
        {
            var res = await _accountRepository.Authenticate(request);
            return Ok(res);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var res = await _accountRepository.RefreshToken(request);
            return Ok(res);
        }

        [HttpPut("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var res = await _accountRepository.ForgotPassword(request);
            return Ok(res);
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var res = await _accountRepository.ResetPassword(request);
            return Ok(res);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var res = await _accountRepository.ChangePassword(request);
            return Ok(res);
        }

        [HttpPut("resend-otp")]
        public async Task<IActionResult> ResendOtp(ResendOTPRequest request)
        {
            var res = await _accountRepository.ResendOTP(request);
            return Ok(res);
        }

    }
}
