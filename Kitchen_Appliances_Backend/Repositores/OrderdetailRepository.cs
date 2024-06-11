using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Order;
using Kitchen_Appliances_Backend.DTO.OrderDetail;
using Kitchen_Appliances_Backend.Interfaces;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class OrderdetailRepository : IOrderdetailRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OrderdetailRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateOrderDetails(List<int> cartDetailIds)
        {
            foreach(int i in cartDetailIds)
            {
                var cartDetail = await _context.CartDetails.FindAsync(i);

            }
            throw new NotImplementedException();
        }

		public async Task<ApiResponse<List<OrderDetailDTO>>> GetAllOrderDetailsByOrder(int orderId)
		{
            try
            {
                var orderDetails = _context.Orderdetails.Where(x => x.OrderId == orderId).ToList();
                return new ApiResponse<List<OrderDetailDTO>>()
                {
                    Status = 200,
                    Message = "Lấy Danh Sách Chi Tiết Đơn Hàng thành công",
                    Data = _mapper.Map<List<OrderDetailDTO>>(orderDetails)
                };
            }
            catch (Exception)
            {
                return new ApiResponse<List<OrderDetailDTO>>()
                {
                    Status = 500,
                    Message = "Lấy danh sách Chi tiết Đơn Hàng thất bại",
                    Data = null
                };
            }
		}

		public async Task<ApiResponse<OrderDetailDTO>> GetOrderDetailById(int orderDetailId)
		{
			try
			{
                var orderDetail = await _context.Orderdetails.FindAsync(orderDetailId);
				return new ApiResponse<OrderDetailDTO>()
				{
					Status = 200,
					Message = "Lấy Danh Sách Chi Tiết Đơn Hàng thành công",
					Data = _mapper.Map<OrderDetailDTO>(orderDetail)
				};
			}
			catch (Exception)
			{
				return new ApiResponse<OrderDetailDTO>()
				{
					Status = 500,
					Message = "Lấy danh sách Chi tiết Đơn Hàng thất bại",
					Data = null
				};
			}
		}
	}
}
