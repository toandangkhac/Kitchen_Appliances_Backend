﻿using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Exceptions;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Customer;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CustomerRepository(DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<ApiResponse<bool>> CreateCustomer(CreateCustomerRequest request)
        {
            try
            {
                var customer = _mapper.Map<Customer>(request);

                var roleEmployee = await _dataContext.Roles.FindAsync(2);

                var account = new Account()
                {
                    Email = request.Email,
                    Password = request.Password,
                    Role = roleEmployee,
                    RoleId = roleEmployee.Id,
                    Status = false
                };

                account.Customers.Add(customer);
                _dataContext.Accounts.Add(account);
                _dataContext.Customers.Add(customer);
                await _dataContext.SaveChangesAsync();
                return new ApiResponse<bool>(200, "Tạo customer thành công", true);
            }
            catch (Exception)
            {
                return new ApiResponse<bool>(400, "Tạo customer lỗi", true); ;
            }
        }

        public async Task<ApiResponse<bool>> DeleteCustomerById(int id)
        {
            var customer = await _dataContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return new ApiResponse<bool>(404, "Không tìm thấy customer", false);
            }

            var account =  _dataContext.Accounts.FirstOrDefault(x => x.Email.Equals(customer.Email));
            if (account == null)
            {
                return new ApiResponse<bool>(404, "Không tìm thấy account customer", false);
            }
            _dataContext.Customers.Remove(customer);
            _dataContext.Accounts.Remove(account);
            await _dataContext.SaveChangesAsync();
            return new ApiResponse<bool>()
            {
                Status = 200,
                Message = "Xóa customer thành công",
                Data = true
            };
        }

        public async Task<ApiResponse<CustomerDTO>> GetCustomerById(int id)
        {
            var customer = _dataContext.Customers.Find(id);
            if(customer == null)
            {
                return new ApiResponse<CustomerDTO>(404, "Không tìm thấy customer", null);
            }
            var customerDto = _mapper.Map<CustomerDTO>(customer);

            return new ApiResponse<CustomerDTO>(200, "Lấy thành công", customerDto);
        }



        public async Task<ApiResponse<List<CustomerDTO>>> ListCustomer()
        {
            try
            {
                var customers = _dataContext.Customers.ToList();
                var customerDtos = new List<CustomerDTO>();
                customers.ForEach(customer => customerDtos.Add(_mapper.Map<CustomerDTO>(customer)));
                return new ApiResponse<List<CustomerDTO>>(200, "Lấy danh sách thành công", customerDtos);
            }
            catch( Exception )
            {
                return new ApiResponse<List<CustomerDTO>>(200, "Lấy danh sách thành công", new List<CustomerDTO>());
            }
        }

        public async Task<ApiResponse<bool>> UpdateCustomer(int id, UpdateCustomerRequest request)
        {
            try
            {
                var customer = _dataContext.Customers.Find(id);
                if (customer == null)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 404,
                        Message = "Không tìm thấy customer",
                        Data = false
                    };
                }
                if (request.PhoneNumber != null)
                {
                    customer.PhoneNumber = request.PhoneNumber;
                }

                if (request.Fullname != null)
                {
                    customer.Fullname = request.Fullname;
                }
                if (request.Address != null)
                {
                    customer.Address = request.Address;
                }
                //customer = _mapper.Map<Customer>(request);
                _dataContext.Customers.Update(customer);
                await _dataContext.SaveChangesAsync();
                return new ApiResponse<bool>()
                {
                    Status = 200,
                    Message = "Update customer thành công",
                    Data = true
                };
            }
            catch ( Exception )
            {
                return new ApiResponse<bool>()
                {
                    Status = 400,
                    Message = "Update customer không thành công",
                    Data = false
                };
            }
			
        }
    }
}
