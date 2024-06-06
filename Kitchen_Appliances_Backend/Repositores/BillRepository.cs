using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Bill;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;

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
        public async Task<ApiResponse<bool>> savePaymentInfor(CreateBillRequest billRequest)
        {
            var order = _context.Orders.Where(x => x.Id == billRequest.OrderId).FirstOrDefault();
            var bill = _mapper.Map<Bill>(billRequest);
            if (order == null)
            {
                return new ApiResponse<bool>()
                {
                    Status = 404,
                    Message = "Không tìm thấy đơn hàng",
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
            bill.Order = order;
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
