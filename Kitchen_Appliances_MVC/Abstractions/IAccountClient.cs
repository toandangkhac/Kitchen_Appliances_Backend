using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.ViewModels.Account;
using Kitchen_Appliances_MVC.ViewModels.Customer;

namespace Kitchen_Appliances_MVC.Abstractions
{
    public interface IAccountClient 
    {
        //Task<AccountDTO> findAccount(string email);
        Task<APIResponse<AccountDTO>> findAccount(string email);

        Task<APIResponse<AuthDTO>> login(LoginAuthRequest request);

        Task<APIResponse<bool>> ForgotPassword(ForgotPasswordRequest request);

        Task<APIResponse<bool>>ResetPassword(ResetPasswordRequest request);

        Task<APIResponse<bool>> ChangePassword(ChangePasswordRequest request);

        Task<APIResponse<bool>> CheckEmail(string Email);

        Task<APIResponse<bool>> ActiveAccount(ActiveAccountRequest request);

        Task<APIResponse<bool>> ResendOTP(ResendOTPRequest request);
    }
}
