using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Enums;
using Kitchen_Appliances_Backend.Commons.Exceptions;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.DTO.Employee;
using Kitchen_Appliances_Backend.DTO.Mail;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;
using Kitchen_Appliances_Backend.Services;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

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

        public async Task<bool> ActiveAccount(VerifyOTPRequest request)
        {
            await _accountRepository.VerifyOTP(request);

            var account = _dataContext.Accounts.FirstOrDefault(x => x.Email == request.Email);

            account.Status = true;

            _dataContext.Accounts.Update(account);
            _dataContext.SaveChanges();

            return true;
        }

        public async Task<bool> CreateEmployee(CreateEmployeeRequest request)
        {
            Employee employee = _mapper.Map<Employee>(request);
            // Quản trị viên, Khách hàng, Nhân viên
            var roleEmployee = await _dataContext.Roles.FindAsync(3);
            var account = new Account()
            {
                Email = request.Email,
                Password = request.Password,
                Role = roleEmployee,
                RoleId = roleEmployee.Id,
                Status = false
            };
            // image dưới database có lỗi cần đưa về nvarchar(100) để có thể lưu ảnh
            if(request.Image != null)
            {
                employee.Image = await _upload.UploadFile(request.Image);
            }

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
                ExpiredAt = DateTime.Now.AddMinutes(TOKEN_TYPE.OTP_EXPIRY_MINUTES)
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
            return true;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _dataContext.Employees.FindAsync(id)
                ?? throw new NotFoundException("Khong tim thay employee by " + id);

            var account = _dataContext.Accounts.FirstOrDefault(x => x.Email == employee.Email);
            account.Status = false;
            _dataContext.Accounts.Update(account);
            _dataContext.SaveChanges();
            return true;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var res = await  _dataContext.Employees.FindAsync(id);
            if (res == null)
                throw new NotFoundException("Not find employee with id: " + id);
            return res;
        }

        public ICollection<Employee> ListEmployee()
        {
            return _dataContext.Employees.ToList();
        }

        public Task<ICollection<Employee>> PagingEmployee(int? page, int? size)
        {
            int pageIndex = page ?? 1;
            int pageSize = size ?? 5;

            throw new NotImplementedException();
        }

        public async Task<bool> UpdateEmployee(int productId, UpdateEmployeeRequest request)
        {
            var employee = await _dataContext.Employees.FindAsync(productId)
                ?? throw new NotFoundException("Khong tim thay san pham by " + productId);

            employee = _mapper.Map(request, employee);

            if(request.Image != null)
            {
                employee.Image = await _upload.UploadFile(request.Image);
            }
            _dataContext.Employees.Update(employee);
            _dataContext.SaveChanges();
            return true;
        }
    }
}
