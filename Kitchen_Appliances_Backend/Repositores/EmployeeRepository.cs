using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Exceptions;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Employee;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;
using Kitchen_Appliances_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IUploadService _upload;

        public EmployeeRepository(DataContext dataContext, IMapper mapper, IUploadService upload)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _upload = upload;
        }

        public async Task<int> CreateEmployee(CreateEmployeeRequest request)
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
            return employee.Id;
        }

        public async Task<int> DeleteEmployee(int id)
        {
            var employee = await _dataContext.Employees.FindAsync(id)
                ?? throw new NotFoundException("Khong tim thay san pham by " + id);

            _dataContext.Employees.Remove(employee);
            _dataContext.SaveChanges();
            return employee.Id;
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

        public async Task<int> UpdateEmployee(int productId, UpdateEmployeeRequest request)
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
            return employee.Id;
        }
    }
}
