using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Bill;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Kitchen_Appliances_Backend.Repositores
{
    
    public class BillRepository : IBillRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public BillRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

		public async Task<ApiResponse<List<ListBillDto>>> GetAllBill()
		{
            var bills = _context.Bills.ToList();
            return new ApiResponse<List<ListBillDto>>()
            {
                Status = 200,
                Message = "Lấy danh sách hóa đơn thành công",
                Data = _mapper.Map<List<ListBillDto>>(bills)
            };
		}

		public async Task<ApiResponse<BillDto>> GetBillInformation(int billId)
		{
            
            var bill = await _context.Bills.FindAsync(billId);
			if (bill == null)
			{
				return new ApiResponse<BillDto>()
				{
					Status = 404,
					Message = "Không tìm hóa đơn",
					Data = null
				};
			}
            var order = await _context.Orders.FindAsync(bill.OrderId);
            var customer = await _context.Customers.FindAsync(order.CustomerId);
            var employee = await _context.Employees.FindAsync(order.EmployeeId);
            var billDto = new BillDto()
            {
                OrderId = billId,
                PaymentTime = bill.PaymentTime,
                CustomerId = order.CustomerId,
                CustomerName = customer.Fullname,
                EmployeeId = employee.Id,
                EmployeeName = employee.Fullname,
                Total = bill.Total,
            };
			return new ApiResponse<BillDto>()
			{
				Status = 200,
				Message = "In hóa đơn thành công",
				Data = billDto
			};
		}

		public async Task<ApiResponse<bool>> savePaymentInfor(int orderId)
        {
            var order = _context.Orders.Where(x => x.Id == orderId).FirstOrDefault();
     
            if (order == null)
            {
                return new ApiResponse<bool>()
                {
                    Status = 404,
                    Message = "Không tìm thấy đơn hàng, vui lòng kiểm tra lại",
                    Data = false
                };
            }
            if (order.PaymentStatus == true)
            {
                return new ApiResponse<bool>()
                {
                    Status = 400,
                    Message = "Đơn hàng đã được thanh toán",
                    Data = false
                };
            }
            //edit

            decimal total = 0;
            var orderDetails = _context.Orderdetails.Where(x => x.OrderId == order.Id).ToList();
            foreach(var orderDetail in orderDetails)
            {
                total += orderDetail.Price;
            }

            var bill = new Bill()
            {
                OrderId = orderId,
                Order = order,
                PaymentTime = DateTime.Now,
                Total = total
            };

            _context.Bills.Add(bill);
            order.PaymentStatus = true;
            _context.Orders.Update(order);
            _context.SaveChanges();
            return new ApiResponse<bool>()
            {
                Status = 200,
                Message = "Lưu thông tin thanh toán thành công",
                Data = false
            };
        }
    }
}
