using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Enums;
using Kitchen_Appliances_Backend.Commons.Exceptions;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.DTO.Mail;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;
using Kitchen_Appliances_Backend.Services;
using System.Security.Claims;

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

        public async Task<ApiResponse<AuthDTO>> Authenticate(LoginAuthRequest request)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Email == request.Email);
            if(account == null)
            {
                return new ApiResponse<AuthDTO>()
                {
                    Status = 404,
                    Message = "Email không tồn tại",
                    Data = null
                };
            }

            if (!account.Password.Equals(request.Password))
            {
                return new ApiResponse<AuthDTO>()
                {
                    Status = 404,
                    Message = "Sai mật khẩu",
                    Data = null
                };
            }

            if (!account.Status)
            {
                return new ApiResponse<AuthDTO>()
                {
                    Status = 500,
                    Message = "Tài khoản đã bị khóa",
                    Data = null
                };
            }

            string access_token = await _jwtService.CreateJWT(account.Email);
            string refresh_token = _jwtService.CreateRefreshToken();

            var response =  new AuthDTO() { AccessToken = access_token, RefreshToken = refresh_token };
            return new ApiResponse<AuthDTO>()
            {
                Status = 200,
                Message = "Login thành công",
                Data = response
            };
        }
        
        public async Task<ApiResponse<bool>> ChangePassword(ChangePasswordRequest request)
        {
            // Yêu cầu phải đăng nhập trước mới có thể thay đổi mật khẩu
            //string username = _currentUserService.UserName;

            if(!request.NewPassword.Equals(request.ConfirmPassword))
            {
                return new ApiResponse<bool>()
                {
                    Status = 409,
                    Message = "Mật khẩu xác thực không trùng với mật khẩu mới",
                    Data = false
                };
            }

            var account = _context.Accounts.FirstOrDefault(x => x.Email == request.Email);
            //?? throw new NotFoundException("Not find account by email, try again!!!");
            if(account == null)
            {
				return new ApiResponse<bool>()
				{
					Status = 404,
					Message = "Không tìm thấy tài khoản",
					Data = false
				};
			}    

            if(!account.Password.Equals(request.OldPassword))
            {
				return new ApiResponse<bool>()
				{
					Status = 400,
					Message = "Mật khẩu cũ bạn nhập không đúng",
					Data = false
				};
			}    

            account.Password = request.NewPassword;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
			return new ApiResponse<bool>()
			{
				Status = 200,
				Message = "Đổi mật khẩu mới thành công",
				Data = true
			};
		}
        public async Task<ApiResponse<List<AccountDTO>>> listAccount()
        {
            var accounts = _mapper.Map<List<AccountDTO>>(_context.Accounts.ToList());
			return new ApiResponse<List<AccountDTO>>()
			{
				Status = 200,
				Message = "Lấy danh sách tài khoản thành công",
				Data = accounts
			};
		}

        public async Task<ApiResponse<AccountDTO>> findAccount(string email)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Email == email);
            if (account == null)
            {
                return new ApiResponse<AccountDTO>()
                {
                    Status = 404,
                    Message = "Không tìm thấy tài khoản với email đấy",
                    Data = null
				};
			}
			return new ApiResponse<AccountDTO>()
			{
				Status = 200,
				Message = "Lấy tài khoản thành công",
				Data = _mapper.Map<AccountDTO>(account)
		    };
		}

        public async Task<ApiResponse<bool>> ForgotPassword(ForgotPasswordRequest request)
        {

            var account = _context.Accounts.FirstOrDefault(x => x.Email.Equals(request.Email));
            if(account == null)
            {
				return new ApiResponse<bool>()
				{
					Status = 404,
					Message = "Không tìm thấy email này trong hệ thống",
					Data = false
				};
			}    


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
                Name = "Khách hàng",
                OTP = otp,
                Title = "Quên mật khẩu",
                Type = MAIL_TYPE.FORGOT_PASSWORD
            });
			return new ApiResponse<bool>()
			{
				Status = 200,
				Message = "Forgot password thành công",
				Data = true
			};
		}

        // 
        public Task<ApiResponse<AuthDTO>> RefreshToken(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<bool>> ResendOTP(ResendOTPRequest request)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Email == request.Email);
            var userToken = _context.AppUserTokens.FirstOrDefault(x => x.Type == request.Type && x.AccountId == request.Email);

            if (userToken == null && request.Type == TOKEN_TYPE.REGISTER_OTP)
            {
				return new ApiResponse<bool>()
				{
					Status = 404,
					Message = "This user hasn't been signed up before, invalid request",
					Data = false
				};
			}

            if (userToken == null && request.Type == TOKEN_TYPE.FORGOT_PASSWORD_OTP)
            {
				return new ApiResponse<bool>()
				{
					Status = 500,
					Message = "You need to perform forgot password feature for your account before, invalid request",
					Data = false
				};
			}    
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
			return new ApiResponse<bool>()
			{
				Status = 200,
				Message = "Gửi lại mã OTP thành công",
				Data = true
			};
		}

        public async Task<ApiResponse<bool>> ResetPassword(ResetPasswordRequest request)
        {

            var result = await VerifyOTP(new VerifyOTPRequest()
            {
                Email = request.Email,
                OTP = request.OTP,
                Type = TOKEN_TYPE.FORGOT_PASSWORD_OTP
            });
            if(result.Data == false)
            {
                return result;
            }    

            var account = _context.Accounts.FirstOrDefault(x => x.Email == request.Email);
            if(account.Status == false)
            {
                return new ApiResponse<bool>()
                {
                    Status = 400,
                    Message = "Tài khoản đã bị khóa",
                    Data = false
                };
            }    
            account.Password = request.Password;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
			return new ApiResponse<bool>()
			{
				Status = 200,
				Message = "Reset password thành công",
				Data = true
			};
		}

        public async Task<ApiResponse<bool>> VerifyOTP(VerifyOTPRequest request)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Email == request.Email);

            if (account == null) {
				return new ApiResponse<bool>()
				{
					Status = 404,
					Message = "Không tìm thấy account",
					Data = false
				};
            }

            var userToken = _context.AppUserTokens.FirstOrDefault(x => x.Type == request.Type
                    && x.AccountId == request.Email);
            if(userToken == null)
            {
                return new ApiResponse<bool>()
                {
                    Status = 404,
                    Message = "Không tìm thấy OTP, Bạn hãy thử lại",
                    Data = false
                };
            }    

            if(userToken.Token != request.OTP)
            {
				return new ApiResponse<bool>()
				{
					Status = 400,
					Message = "YOTP is invalid",
					Data = false
				};
            }

            if (userToken.ExpiredAt <= DateTime.Now)
            {
				return new ApiResponse<bool>()
				{
					Status = 400,
					Message = "OTP is expired",
					Data = false
				};
            }
			return new ApiResponse<bool>()
			{
				Status = 200,
				Message = "Verify OTP thành công",
				Data = true
			};
		}

        public async Task<ApiResponse<string>> validateExpiredJwt(string token)
        {
            if(token == null)
            {
                throw new InvalidRequestException("");
            }    
            var claims = _jwtService.validateExpiredJwt(token);
            return new ApiResponse<string>()
            {
                Status = 200,
                Message = "validate expired jwt",
                //Data = claims.FindFirstValue("Email")
                Data = claims.FindFirstValue(ClaimTypes.Role)
			};
        }

        public async Task<ApiResponse<bool>> ActiveAccount(ActiveAccountRequest request)
        {
            var result = await VerifyOTP(new VerifyOTPRequest()
            {
                Email = request.Email,
                OTP = request.OTP,
                Type = TOKEN_TYPE.REGISTER_OTP
            });
            if (result.Data == false)
            {
                return result;
            }
            var account = _context.Accounts.FirstOrDefault(x => x.Email == request.Email);
            account.Status = true;// cập nhật tài khoản
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>()
            {
                Status = 200,
                Message = "Active account thành công",
                Data = true
            };
        }
    }
}
