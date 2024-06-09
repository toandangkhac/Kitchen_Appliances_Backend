using Kitchen_Appliances_MVC.Abstractions;
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
            LoginAuthRequest request = new LoginAuthRequest()
            {
                Email = "chientran@gmail.com",
                Password = "1234"
            };
           
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginAuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            AuthDTO authDTO = _accountClient.login(request).Result;
            Response.Cookies.Append("jwt-token", authDTO.AccessToken, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddMinutes(60)
            });
            Console.WriteLine(authDTO);
            return RedirectToAction("Index", "Home");
        }

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

        public IActionResult ForgotPassword()
        {
            ForgotPasswordRequest request = new ForgotPasswordRequest()
            {
                Email = "chientran.nt196@gmail.com"
            };
            var res  = _accountClient.ForgotPassword(request);
            Console.WriteLine(res.Result);
            return View();
        }

        public IActionResult Detail()
        {
            string email = "chientran@gmail.com";
            AccountDTO user = _accountClient.findAccount(email).Result;
            Console.WriteLine(user.Email);
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

        public IActionResult ResetPassword()
        {
            ResetPasswordRequest request = new ResetPasswordRequest();
            var res = _accountClient.ResetPassword(request);
            return View(res);
        }

        public IActionResult Index()
        {
            
            return View();
        }

    }
}
