using Kitchen_Appliances_Backend.DTO.Auth;

namespace Kitchen_Appliances_Backend.Services
{
    public interface IAuthService
    {
        Task<AuthDTO> Authenticate(LoginAuthRequest request);

        Task<AuthDTO> RefreshToken(RefreshTokenRequest request);
    }
}
