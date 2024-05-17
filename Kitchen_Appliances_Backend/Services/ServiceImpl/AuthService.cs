using Kitchen_Appliances_Backend.Commons.Exceptions;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Auth;
using Kitchen_Appliances_Backend.Interfaces;

namespace Kitchen_Appliances_Backend.Services.ServiceImpl
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(DataContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<AuthDTO> Authenticate(LoginAuthRequest request)
        {

            var account = _context.Accounts.FirstOrDefault(x => x.Email == request.Email)
                ?? throw new NotFoundException("Not find account by email, try again!!!");

            if (!account.Password.Equals(request.Password))
            {
                throw new NotFoundException("Password not match, try again!!!");
            }

            if(account.Status)
            {
                throw new InvalidRequestException("Your account has been lockout");
            }

            string access_token = await _jwtService.CreateJWT(account.Email);
            string refresh_token = _jwtService.CreateRefreshToken();

            return new AuthDTO() { AccessToken = access_token, RefreshToken = refresh_token };
        }

        // 
        public Task<AuthDTO> RefreshToken(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
