using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Order;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<ApiResponse<bool>> ConfirmOrder(int employeeId, int orderId)
        {
            var employee = _context.Employees.Where(x => x.Id == employeeId).FirstOrDefault();
            if(employee == null)
            {
                return new ApiResponse<bool>()
                {
                    Status = 404,
                    Message = "Không tìm thấy nhân viên",
                    Data = false
                };
            }
            var order = _context.Orders.Where(x => x.Id ==orderId).FirstOrDefault();
            if (order==null)
            {
                return new ApiResponse<bool>()
                {
                    Status = 404,
                    Message = "Không tìm thấy đơn hàng",
                    Data = false
                };
            }
            // trừ số lượng sản phẩm
            var orderDetails = _context.Orderdetails.Where(x => x.OrderId == orderId).ToList();
            //Check xem có đủ số lượng sản phẩm không
            for (int i = 0; i < orderDetails.Count; i++)
            {
                Product p = _context.Products.Where(x => x.Id == orderDetails[i].ProductId).FirstOrDefault();

                if (p.Quantity < orderDetails[i].Quantity)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 400,
                        Message = "Không đủ sản phẩm để duyệt đơn hàng",
                        Data = false
                    };
                }
            }
            for (int i = 0; i < orderDetails.Count; i++)
            {
                Product p = _context.Products.Where(x => x.Id == orderDetails[i].ProductId).FirstOrDefault();
                p.Quantity -= orderDetails[i].Quantity;
                _context.Products.Update(p);
            }
            order.Employee = employee;
            order.Status = 2;// 0: Đã hủy, 1: Chờ xác nhận, 2: Đang giao, 3: Đã nhận hàng
            _context.Orders.Update(order);
            _context.SaveChanges();

            return new ApiResponse<bool>()
            {
                Status = 200,
                Message = "Duyệt đơn hàng hàng thành công",
                Data = true
            };

        }

        public async Task<ApiResponse<bool>> CreateOrder(int Id)
        {
            var cartDetails = _context.CartDetails.Where(x => x.CustomerId == Id).ToList();
            if (cartDetails.IsNullOrEmpty())
            {
                return new ApiResponse<bool>()
                {
                    Status = 404,
                    Message = "Không tìm thấy cart detail",
                    Data = false
                };
            }
            var customer = _context.Customers.Where(x => x.Id == Id).FirstOrDefault();
            var order = new Order
            {
                CreateAt = DateTime.Now,
                CustomerId = Id,
                Status = 1,
                Customer = customer
            };
            _context.Add(order);
            _context.SaveChanges();
            var newOrder = _context.Orders.OrderByDescending(x => x.CreateAt).FirstOrDefault();

            for (int i = 0; i < cartDetails.Count; i++)
            {
                Product p = _context.Products.Where(x => x.Id == cartDetails[i].ProductId).FirstOrDefault();
                OrderDetail o = new OrderDetail
                {
                    OrderId = newOrder.Id,
                    ProductId = cartDetails[i].ProductId,
                    Quantity = cartDetails[i].Quantity,
                    Price = p.Price,
                    Order = newOrder,
                    Product = p
                };
                _context.Orderdetails.Add(o);
                newOrder.OrderDetails.Add(o);
                _context.CartDetails.Remove(cartDetails[i]);

                

            }
            _context.SaveChanges();

            return new ApiResponse<bool>()
            {
                Status = 200,
                Message = "Đặt hàng thành công",
                Data = true
            };

            //var order = new Order();
            //foreach(int cartIds in request.CartDetailIds)
            //{
            //    var cartDetail = await _context.CartDetails.FindAsync(cartIds);
            //    if (cartDetail == null)
            //    {
            //        return new ApiResponse<bool>()
            //        {
            //            Status = 404,
            //            Message = "Không tìm thấy cart detail",
            //            Data = false
            //        };
            //    }
            //    var product = await _context.Products.FindAsync(cartDetail.ProductId);


            //    var orderDetail = new OrderDetail();
            //    orderDetail.Price = cartDetail.Quantity * product.Price;
            //    orderDetail.Quantity = cartDetail.Quantity;
            //    orderDetail.Product = product;
            //    orderDetail.ProductId = cartDetail.ProductId;

            //    orderDetail.Order = order;


            //}
            //throw new NotImplementedException();
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
