using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Enums;
using Kitchen_Appliances_Backend.Commons.Exceptions;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.DTO.Mail;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;
using Kitchen_Appliances_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        private readonly IJwtService _jwtService;
        private readonly IOtpService _otpService;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public AccountRepository(DataContext context, IJwtService jwtService, ICurrentUserService currentUserService
            , IOtpService otpService, IMailService mailService, IMapper mapper)
        {
            _context = context;
            _jwtService = jwtService;
            _otpService = otpService;
            _mailService = mailService;
            _mapper = mapper;
            _currentUserService = currentUserService;
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
        
        public async Task<bool> ChangePassword(ChangePasswordRequest request)
        {
            // Yêu cầu phải đăng nhập trước mới có thể thay đổi mật khẩu
            //string username = _currentUserService.UserName;

            string username = "chientran@gmail.com";

            if(!request.NewPassword.Equals(request.ConfirmPassword))
            {
                throw new InvalidRequestException("");
            }

            var account = _context.Accounts.FirstOrDefault(x => x.Email == username)
                ?? throw new NotFoundException("Not find account by email, try again!!!");

            if(!account.Password.Equals(request.OldPassword))
            {
                throw new InvalidRequestException("Old password isn't match");
            }    

            account.Password = request.NewPassword;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<AccountDTO>> listAccount()
        {
            var accounts = _mapper.Map<List<AccountDTO>>(_context.Accounts);
            return accounts;
        }

        public async Task<AccountDTO> findAccount(string email)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Email == email);
            if (account == null)
            {
                throw new NotFoundException("Can't find account by email");
            }
            return _mapper.Map<AccountDTO>(account);
        }
        [Authorize]
        public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
        {

            var account = _context.Accounts.FirstOrDefault(x => x.Email.Equals(request.Email))
                ?? throw new NotFoundException("Không tìm thấy email này trong hệ thống");

            var employee = _context.Employees.FirstOrDefault(x => x.Email.Equals(request.Email))
                ?? throw new NotFoundException("Không tìm thấy email này trong hệ thống");

            var userToken = _context.AppUserTokens.FirstOrDefault(x => x.Type == 
                        TOKEN_TYPE.FORGOT_PASSWORD_OTP && x.AccountId == account.Email);

            var otp = _otpService.GenerateOTP();

            if(userToken is null)
            {
                userToken = new AppUserToken()
                {
                    Token = otp,
                    Type = TOKEN_TYPE.FORGOT_PASSWORD_OTP,
                    ExpiredAt = DateTime.Now.AddMinutes(TOKEN_TYPE.OTP_EXPIRY_MINUTES)
                };
                account.AppUserTokens.Add(userToken);
            }
            else
            {
                userToken.Token = otp;
                userToken.ExpiredAt = DateTime.Now.AddMinutes(TOKEN_TYPE.OTP_EXPIRY_MINUTES);

            }
            
            _context.AppUserTokens.Update(userToken);
            _context.SaveChangesAsync();
            _mailService.sendMail(new DTO.Mail.CreateMailRequest()
            {
                Email = account.Email,
                Name = employee.Fullname,
                OTP = otp,
                Title = "Quên mật khẩu",
                Type = MAIL_TYPE.FORGOT_PASSWORD
            });
            return true;
        }

        // 
        public Task<AuthDTO> RefreshToken(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ResendOTP(ResendOTPRequest request)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Email == request.Email);
            var userToken = _context.AppUserTokens.FirstOrDefault(x => x.Type == request.Type && x.AccountId == request.Email);

            if (userToken == null && request.Type == TOKEN_TYPE.REGISTER_OTP)
                throw new InvalidRequestException("This user hasn't been signed up before, invalid request");

            if (userToken == null && request.Type == TOKEN_TYPE.FORGOT_PASSWORD_OTP)
                throw new InvalidRequestException("You need to perform forgot password feature for your account before, invalid request");

            var otp = _otpService.GenerateOTP();
            
            userToken.Token = otp;
            userToken.ExpiredAt = DateTime.Now.AddMinutes(TOKEN_TYPE.OTP_EXPIRY_MINUTES);

            _mailService.sendMail(new CreateMailRequest()
            {
                Name = account.Email,
                Type = MAIL_TYPE.RESEND,
                OTP = otp,
                Title = "Gửi lại mã OTP",
                Email = account.Email
            });
            _context.AppUserTokens.Update(userToken);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {

            await VerifyOTP(new VerifyOTPRequest()
            {
                Email = request.Email,
                OTP = request.OTP,
                Type = TOKEN_TYPE.FORGOT_PASSWORD_OTP
            });

            var account = _context.Accounts.FirstOrDefault(x => x.Email == request.Email);

            account.Password = request.Password;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> VerifyOTP(VerifyOTPRequest request)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Email == request.Email);

            if (account == null) {
                throw new NotFoundException("Không tìm thấy account by email : " + request.Email);
            }

            var userToken = _context.AppUserTokens.FirstOrDefault(x => x.Type == request.Type
                    && x.AccountId == request.Email) ?? throw new NotFoundException();

            if(userToken.Token != request.OTP)
            {
                throw new InvalidRequestException("OTP is invalid");
            }

            if (userToken.ExpiredAt <= DateTime.Now)
            {
                throw new InvalidRequestException("OTP is expired");
            }
            return true;
        }
    }
}
