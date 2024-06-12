using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.Options;
using Kitchen_Appliances_MVC.ViewModelData.Customer;
using Kitchen_Appliances_MVC.ViewModelData.Header;
using Kitchen_Appliances_MVC.ViewModels.Account;
using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Customer;
using Kitchen_Appliances_MVC.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly ICustomerServiceClient _customerServiceClient;
        private readonly IEmployeeClient _employeeClient;
        private readonly ICartDetailServiceClient _cartDetailServiceClient;
        private readonly ICategoryServiceClient _categoryServiceClient;
        public AccountController(IAccountClient accountClient, IConfiguration configuration, 
            ICustomerServiceClient customerServiceClient, IEmployeeClient employeeClient, ICartDetailServiceClient cartDetailServiceClient, ICategoryServiceClient categoryServiceClient)
        {
            _accountClient = accountClient;
            _configuration = configuration;
            _customerServiceClient = customerServiceClient;
            _employeeClient = employeeClient;
            _cartDetailServiceClient = cartDetailServiceClient;
            _categoryServiceClient = categoryServiceClient;
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
                var user = await _employeeClient.GetListAll();
                if(user.Status != 200)
                {
                    Console.WriteLine(user.Message);
                }
                List<EmployeeDTO> employees = user.Data;
                int UserId = employees.Where(e => e.Email == username).FirstOrDefault().Id;
                HttpContext.Session.SetString("IdUser", UserId.ToString());
				HttpContext.Session.SetString("RoleId", "1");
				return RedirectToAction("Index", "Admin");
            }  
            else if(role == "Khách hàng")
            {
                var user = await _customerServiceClient.ListCustomer();
                if (user.Status != 200)
                {
                    Console.WriteLine(user.Message);
                }
                List<CustomerDTO> customers = user.Data;
                int UserId = customers.Where(c => c.Email == username).FirstOrDefault().Id;
                HttpContext.Session.SetString("IdUser", UserId.ToString());
                HttpContext.Session.SetString("RoleId", "2");
                var dataCart = await _cartDetailServiceClient.GetCartDetailByCustomer(UserId);
                if (dataCart.Status != 200)
                {
                    Console.WriteLine(dataCart.Message);
                }
                int CartCount = dataCart.Data.Count;
                HttpContext.Session.SetString("Cartcount", CartCount.ToString());
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
            HttpContext.Session.Clear();
            Response.Cookies.Delete("jwt-token");
            return RedirectToAction("Index", "Home");
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
		public async Task<ActionResult> ChangeInfo(String id)
		{
			int IdUser = int.Parse(id);
			var dataCategories = await _categoryServiceClient.GetAllCategories();
			if (dataCategories.Status != 200)
			{
				Console.WriteLine(dataCategories.Message);
			}
			List<CategoryDTO> categories = dataCategories.Data;
			var dataCustomer = await _customerServiceClient.GetCustomerById(IdUser);
			if (dataCustomer.Status != 200)
			{
				Console.WriteLine(dataCustomer.Message);
			}
			CustomerDTO customer = dataCustomer.Data;
			var headerViewModel = new HeaderViewModel()
			{
				Categories = categories
			};
			ViewBag.HeaderData = headerViewModel;
			ChangeInfoViewModel Model = new ChangeInfoViewModel()
			{
				Categories = categories,
				Customer = customer
			};
			return View(Model);
		}
		[HttpPost]
		public async Task<ActionResult> CompleteChange(string Id, string Fullname, string PhoneNumber, string Address)
		{
			int IdCustomer = int.Parse(Id);
			Console.WriteLine(IdCustomer);
			UpdateCustomerRequest request = new UpdateCustomerRequest()
			{
				Fullname = Fullname,
				PhoneNumber = PhoneNumber,
				Address = Address
			};
            var checkUpdate = await _customerServiceClient.UpdateCustomer(IdCustomer, request);
			if (checkUpdate.Status != 200)
			{
				Console.WriteLine(checkUpdate.Message);
			}
			return Json(new
			{
				IdCustomer
			});
		}
	}
}
