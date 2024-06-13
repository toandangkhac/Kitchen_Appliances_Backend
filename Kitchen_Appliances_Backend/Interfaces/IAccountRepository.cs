using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Account;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IAccountRepository
    {
        //Task<List<AccountDTO>> listAccount();
        Task<ApiResponse<List<AccountDTO>>> listAccount();

		//Task<AccountDTO> findAccount(string email);
        Task<ApiResponse<AccountDTO>> findAccount(string email);

    	//Task<string> validateExpiredJwt(string token);
        Task<ApiResponse<string>> validateExpiredJwt(string token);

		//Task<AuthDTO> Authenticate(LoginAuthRequest request);
        Task<ApiResponse<AuthDTO>> Authenticate(LoginAuthRequest request);

        //Task<AuthDTO> RefreshToken(RefreshTokenRequest request);
        Task<ApiResponse<AuthDTO>> RefreshToken(RefreshTokenRequest request);

        //Task<bool> ResendOTP(ResendOTPRequest request);
        Task<ApiResponse<bool>> ResendOTP(ResendOTPRequest request);

        Task<ApiResponse<bool>> VerifyOTP(VerifyOTPRequest request);

        Task<ApiResponse<bool>> ForgotPassword(ForgotPasswordRequest request);

        Task<ApiResponse<bool>> ResetPassword(ResetPasswordRequest request);

        Task<ApiResponse<bool>> ChangePassword(ChangePasswordRequest request);

        Task<ApiResponse<bool>> ActiveAccount(ActiveAccountRequest request);
    }
}
