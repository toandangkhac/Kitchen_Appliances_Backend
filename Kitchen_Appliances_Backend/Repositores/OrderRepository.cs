using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Order;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public Task<ApiResponse<bool>> CancelOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<bool>> CreateOrder(CreateOrderRequest request)
        {
            var order = new Order();
            foreach(int cartIds in request.CartDetailIds)
            {
                var cartDetail = await _context.CartDetails.FindAsync(cartIds);
                if (cartDetail == null)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 404,
                        Message = "Không tìm thấy cart detail",
                        Data = false
                    };
                }
                var product = await _context.Products.FindAsync(cartDetail.ProductId);


                var orderDetail = new OrderDetail();
                orderDetail.Price = cartDetail.Quantity * product.Price;
                orderDetail.Quantity = cartDetail.Quantity;
                orderDetail.Product = product;
                orderDetail.ProductId = cartDetail.ProductId;

                orderDetail.Order = order;
                
                
            }
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<List<OrderDTO>>> ListOrderByCustomer(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if(customer == null)
            {
                return new ApiResponse<List<OrderDTO>>()
                {
                    Status = 404,
                    Message = "Không tìm thấy người dùng",
                    Data = null
                };
            }

            var orders = _context.Orders.Where(x => x.Customer == customer).ToList();
            var orderDtos = _mapper.Map<List<OrderDTO>>(orders);

            return new ApiResponse<List<OrderDTO>>()
            {
                Status = 200,
                Message = "Lấy danh sách order thành công",
                Data = orderDtos
            };
        }

        public Task<ApiResponse<List<OrderDTO>>> ListOrderConfirmed()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<OrderDTO>>> ListOrderNotConfirm()
        {
            throw new NotImplementedException();
        }
    }
}
