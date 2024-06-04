using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.Models;
using System.Security.Claims;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<AccountDTO>> listAccount();

        Task<AccountDTO> findAccount(string email);

        Task<string> validateExpiredJwt(string token);

        Task<AuthDTO> Authenticate(LoginAuthRequest request);

        Task<AuthDTO> RefreshToken(RefreshTokenRequest request);

        Task<bool> ResendOTP(ResendOTPRequest request);

        Task<bool> VerifyOTP(VerifyOTPRequest request);

        Task<bool> ForgotPassword(ForgotPasswordRequest request);

        Task<bool> ResetPassword(ResetPasswordRequest request);

        Task<bool> ChangePassword(ChangePasswordRequest request);
    }
}
