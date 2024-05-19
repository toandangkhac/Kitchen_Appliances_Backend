using Kitchen_Appliances_Backend.Commons.Enums;
using Kitchen_Appliances_Backend.Commons.Exceptions;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;
using Kitchen_Appliances_Backend.Services;
using Newtonsoft.Json.Linq;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        private readonly IJwtService _jwtService;
        private readonly IOtpService _otpService;
        private readonly IMailService _mailService;

        public AccountRepository(DataContext context, IJwtService jwtService
            , IOtpService otpService, IMailService mailService)
        {
            _context = context;
            _jwtService = jwtService;
            _otpService = otpService;
            _mailService = mailService;
        }

        public async Task<AuthDTO> Authenticate(LoginAuthRequest request)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Email == request.Email)
                ?? throw new NotFoundException("Not find account by email, try again!!!");

            if (!account.Password.Equals(request.Password))
            {
                throw new NotFoundException("Password not match, try again!!!");
            }

            if (!account.Status)
            {
                throw new InvalidRequestException("Your account has been lockout");
            }

            string access_token = await _jwtService.CreateJWT(account.Email);
            string refresh_token = _jwtService.CreateRefreshToken();

            return new AuthDTO() { AccessToken = access_token, RefreshToken = refresh_token };
        }

        public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
        {

            var account = _context.Accounts.FirstOrDefault(x => x.Email.Equals(request.Email))
                ?? throw new NotFoundException("Không tìm thấy email này trong hệ thống");

            var employee = _context.Employees.FirstOrDefault(x => x.Email.Equals(request.Email))
                ?? throw new NotFoundException("Không tìm thấy email này trong hệ thống");

            var otp = _otpService.GenerateOTP();

            _mailService.sendMail(new DTO.Mail.CreateMailRequest()
            {
                Email = account.Email,
                Name = employee.Fullname,
                OTP = otp,
                Title = "Quên mật khẩu",
                Type = TOKEN_TYPE.FORGOT_PASSWORD_OTP
            });
            return true;
        }

        // 
        public Task<AuthDTO> RefreshToken(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResendOTP(ResendOTPRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
