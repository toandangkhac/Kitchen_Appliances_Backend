using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Enums;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.DTO.Employee;
using Kitchen_Appliances_Backend.DTO.Mail;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;
using Kitchen_Appliances_Backend.Services;


namespace Kitchen_Appliances_Backend.Repositores
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IUploadService _upload;
        private readonly IMailService _mail;
        private readonly IOtpService _otp;
        private readonly IAccountRepository _accountRepository;

        public EmployeeRepository(DataContext dataContext, IMapper mapper, IUploadService upload
            , IMailService mail, IOtpService otp, IAccountRepository accountRepository)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _upload = upload;
            _mail = mail;
            _otp = otp;
            _accountRepository = accountRepository;
        }

        public async Task<ApiResponse<object>> ActiveAccount(VerifyOTPRequest request)
        {
            await _accountRepository.VerifyOTP(request);
            var res = new ApiResponse<object>();
            var account = _dataContext.Accounts.FirstOrDefault(x => x.Email == request.Email);
            if(account == null)
            {
                res.Status = 404;
                res.Message = "Kích hoạt tài khoản không thành công";
                res.Data = false;
                return res;
            }

            account.Status = true;

            _dataContext.Accounts.Update(account);
            _dataContext.SaveChanges();

            res.Status = 200;
            res.Message = "Kích hoạt tài khoản thành công";
            res.Data = true;

            return res;
        }

        public async Task<ApiResponse<object>> CreateEmployee(CreateEmployeeRequest request)
        {
            Employee employee = _mapper.Map<Employee>(request);
            // Quản trị viên, Khách hàng, Nhân viên
            var roleEmployee = await _dataContext.Roles.FindAsync(1);
            var account = new Account()
            {
                Email = request.Email,
                Password = request.Password,
                Role = roleEmployee,
                RoleId = roleEmployee.Id,
                Status = false
            };
            // image dưới database có lỗi cần đưa về nvarchar(100) để có thể lưu ảnh
            //if(request.Image != null)
            //{
            //    employee.Image = await _upload.UploadFile(request.Image);
            //}
            //employee.Image = null;
            employee.EmailNavigation = account;
            account.Employees.Add(employee);

            _dataContext.Add(account);
            _dataContext.Add(employee);
            _dataContext.SaveChanges();
            //generate otp
            var otp = _otp.GenerateOTP();

            //save register otp 
            var userToken = new AppUserToken()
            {
                Token = otp,
                Type = TOKEN_TYPE.REGISTER_OTP,
                ExpiredAt = DateTime.Now.AddMinutes(TOKEN_TYPE.OTP_EXPIRY_MINUTES),
                AccountId = employee.Email,
                Account = account
            };

            _dataContext.AppUserTokens.Add(userToken);
            await _dataContext.SaveChangesAsync();

            //Send mail confirm
            var title = "Xác nhận đăng ký tài khoản";
            var name = employee.Fullname;
            _mail.sendMail(new CreateMailRequest()
            {
                Email = account.Email,
                Name = name,
                OTP = otp,
                Title = title,
                Type = MAIL_TYPE.REGISTATION
            });
            return new ApiResponse<object>(200, "Thực hiện thành công", true);
        }

        public async Task<ApiResponse<object>> DeleteEmployee(int id)
        {
            var employee = await _dataContext.Employees.FindAsync(id);

            var res = new ApiResponse<object>();

            if (employee == null)
            {
                res = new ApiResponse<object>(404, "Không tìm thấy Employee", false);
                return res;
            }

            var account = _dataContext.Accounts.FirstOrDefault(x => x.Email == employee.Email);
            account.Status = false;
            _dataContext.Accounts.Update(account);
            _dataContext.SaveChanges();

            res = new ApiResponse<object>(200, "Xóa employee thành công", true);
            return res;
        }

        public async Task<ApiResponse<object>> GetEmployeeById(int id)
        {
            var employee = await  _dataContext.Employees.FindAsync(id);

            var res = new ApiResponse<object>();
            if (employee == null)
            {
                res = new ApiResponse<object>(404, "Không tìm thấy Employee", null);
                return res;
            }

            var employeeDto = _mapper.Map<DTO.Employee.EmployeeDTO>(employee);

            res = new ApiResponse<object>(200, "Lấy thành công", employeeDto);

            return res;
        }

        public async Task<ApiResponse<object>> ListEmployee()
        {
            // _dataContext.Employees.ToList();
            var employees = _dataContext.Employees.ToList();

            var employeeDtos = new List<EmployeeDTO>();
            employees.ForEach(x => employeeDtos.Add(_mapper.Map<DTO.Employee.EmployeeDTO>(x)));

            var res = new ApiResponse<object>();

            if(employees == null)
            {
                res = new ApiResponse<object>(404,"Danh sách Employee trống", employeeDtos);
            }

            res = new ApiResponse<object>(200, "Lấy Danh sách thành công", employeeDtos);
            return res;
        }

        public async Task<ApiResponse<object>> UpdateEmployee(int productId, UpdateEmployeeRequest request)
        {
            var employee = await _dataContext.Employees.FindAsync(productId);

            if(employee == null)
            {
                 return new ApiResponse<object>(404, "Không tìm thấy employee", null);
            }

            if(request.Fullname != null)
            {
                employee.Fullname = request.Fullname;
            }    
            if(request.PhoneNumber != null)
            {
                employee.PhoneNumber = request.PhoneNumber;
            }    

            if(request.Address  != null)
            {
                employee.Address = request.Address;
            }

            if(request.Image != null)
            {
                employee.Image = await _upload.UploadFile(request.Image);
            }
            _dataContext.Employees.Update(employee);
            _dataContext.SaveChanges();
            return new ApiResponse<object>(200, "Update thành công", true);
        }
    }
}
