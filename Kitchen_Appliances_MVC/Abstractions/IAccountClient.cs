using Kitchen_Appliances_MVC.ViewModels.Account;
using Kitchen_Appliances_MVC.ViewModels.Customer;

namespace Kitchen_Appliances_MVC.Abstractions
{
    public interface IAccountClient 
    {
        Task<AccountDTO> findAccount(string email);

        Task<AuthDTO> login(LoginAuthRequest request);

        Task<bool> ForgotPassword(ForgotPasswordRequest request);

        Task<bool>ResetPassword(ResetPasswordRequest request);

        Task<bool> ChangePassword(ChangePasswordRequest request);

        Task<bool> CheckEmail(string Email);
    }
}
