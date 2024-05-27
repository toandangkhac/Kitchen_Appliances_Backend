using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.DTO;
using Kitchen_Appliances_MVC.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountClient _accountClient;

        public AccountController(IAccountClient accountClient)
        {
            _accountClient = accountClient;
        }

        public IActionResult Login()
        {
            LoginAuthRequest request = new LoginAuthRequest()
            {
                Email = "chientran@gmail.com",
                Password = "1234"
            };
            AuthDTO authDTO = _accountClient.login(request).Result;
            Console.WriteLine(authDTO);
            return View();
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
