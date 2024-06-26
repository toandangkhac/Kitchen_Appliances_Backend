﻿using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.Options;
using Kitchen_Appliances_MVC.ViewModels.Account;
using Kitchen_Appliances_MVC.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kitchen_Appliances_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountClient _accountClient;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountClient accountClient, IConfiguration configuration)
        {
            _accountClient = accountClient;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginAuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.error = "Không được để trống dữ liệu.Try again !!!";
                return View();
            }
            var dataLogin = await _accountClient.login(request);
            if(dataLogin.Status != 200)
            {
                ViewBag.error = dataLogin.Message;
                return View();
            }
            AuthDTO authDTO = dataLogin.Data;
            ClaimsPrincipal claims = ValidateToken(authDTO.AccessToken);
            string username = claims.FindFirstValue("Email");
            string role = claims.FindFirstValue(ClaimTypes.Role);
            string FullName = claims.FindFirstValue("FullName");
            
            HttpContext.Session.SetString("Username", username);
            HttpContext.Session.SetString("Fullname", FullName);
            if (role == "Quản trị viên")
            {
                return View();
            }  
            else if(role == "Khách hàng")
            {
                return RedirectToAction("Index", "Home");
            }
            return null;
        }

        //[HttpPost]
        //public IActionResult Login(LoginAuthRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(ModelState);
        //    }
        //    AuthDTO authDTO = _accountClient.login(request).Result;
        //    Response.Cookies.Append("jwt-token", authDTO.AccessToken, new CookieOptions()
        //    {
        //        HttpOnly = true,
        //        Secure = true,
        //        Expires = DateTime.Now.AddMinutes(60)
        //    });
        //    Console.WriteLine(authDTO);
        //    return RedirectToAction("Index", "Home");
        //}

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt-token");
            return View();
        }
        
        //Get data from token
        private ClaimsPrincipal ValidateToken(string token)
        {
			IdentityModelEventSource.ShowPII = true;

			var jwtOptions = new JwtConfigOptions();
			_configuration.GetSection(nameof(JwtConfigOptions)).Bind(jwtOptions);

			TokenValidationParameters validationParameters = new()
			{
				ValidateLifetime = false,
				ValidateIssuerSigningKey = true,
				ValidAudience = jwtOptions.Issuer,
				ValidIssuer = jwtOptions.Issuer,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
			};

			ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out SecurityToken validatedToken);
			if (validatedToken is not JwtSecurityToken jwtSecurityToken
				|| !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				return null;

			return principal;
		}
        
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.error = "Không được để trống dữ liệu.Try again !!!";
                return View();
            }
            var forgotPassword  = await _accountClient.ForgotPassword(request);
            if(forgotPassword.Status != 200)
            {
                ViewBag.error = forgotPassword.Message;
                return View();
            }    
            
            return RedirectToAction("ResetPassword", "Account");
        }

        [HttpGet] 
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.error = "Không được để trống dữ liệu.Try again !!!";
                return View();
            }
            var resetPassword = await _accountClient.ResetPassword(request);
            if (resetPassword.Status != 200)
            {
                ViewBag.error = resetPassword.Message;
                return View();
            }
            return View();
        }

        // muốn change password phải đăng nhập login trước , phải xử lý đăng nhập 
        public IActionResult ChangePassword()
        {
            // lấy dữ liệu mẫu test
            ChangePasswordRequest request = new ChangePasswordRequest()
            {
                OldPassword = "1234",
                NewPassword = "123456",
                ConfirmPassword = "123456"
            };

            var res = _accountClient.ChangePassword(request);
            Console.WriteLine(res.Result);
            return View();
        }

        public IActionResult Index()
        {
            
            return View();
        }

    }
}
