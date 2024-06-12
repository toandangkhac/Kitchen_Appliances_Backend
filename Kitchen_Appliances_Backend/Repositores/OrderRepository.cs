using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Order;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;
using Kitchen_Appliances_Backend.PaymentService;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
		private readonly IVnPayService _pay;


        // 0: Đã hủy, 1: Chờ thanh toán --thanh toán --> 2: Chờ xác nhận, 3: Đang giao, 4: Đã nhận hàng

        public OrderRepository(DataContext context, IMapper mapper, IVnPayService pay)
        {
            _context = context;
            _mapper = mapper;
			_pay = pay;
        }
		//Người dùng nhấn vào Thanh toán khi nhận hàng
		public async Task<ApiResponse<bool>> ThanhToanKhiNhanHang(int orderId)
		{
			var order = await _context.Orders.FindAsync(orderId);
			if(order == null)
			{
                return new ApiResponse<bool>()
                {
                    Status = 404,
                    Message = "Không tìm thấy đơn hàng",
                    Data = false
                };
            }
            order.Status = 2;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>()
            {
                Status = 200,
                Message = "Cập nhật đơn hàng đã giao thành công",
                Data = true
            };
        }
		//Khách hàng xác nhận Giao Hàng Thành Công set order status == 4
		public async Task<ApiResponse<bool>> ConfirmOrderDeliverySucess(int orderId)
		{
			try
			{
				var order = await _context.Orders.FindAsync(orderId);
                if (order.Status == 0)
                {
					return new ApiResponse<bool>()
					{
						Status = 500,
						Message = "Đơn hàng đã bị hủy",
						Data = false
					};
				}
                order.Status = 4;
				_context.Orders.Update(order);
				await _context.SaveChangesAsync();
				return new ApiResponse<bool>()
				{
					Status = 200,
					Message = "Cập nhật đơn hàng đã giao thành công",
					Data = true
				};
			}
			catch (Exception)
			{
				return new ApiResponse<bool>()
				{
					Status = 500,
					Message = "Cập nhât đơn hàng đã giao thất bại",
					Data = false
				};
			}
		}
		//Nhân viên hủy đơn hàng khi người dùng boom hàng khi đơn hàng đang ở trạng thái giao hàng(3) --> hủy đơn hàng(0)
		//Hoặc người dùng thanh toán online thất bại hệ thống sẻ hủy đơn hàng
		public async Task<ApiResponse<bool>> CancelOrder(int orderId)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
				if(order.Status == 4) // khi người dùng boom hàng sẻ ta tiến hàng cập nhật đơn hàng Đang giao(3) -> Hủy đơn hàng(0) 
				{
					return new ApiResponse<bool>()
					{
						Status = 400,
						Message = "Không thể hủy đơn hàng, do đơn hàng đã hoàn thành",
						Data = false
					};
				}	
                order.Status = 0;

				//cập nhật lại số lượng sản phẩm
				var orderDetails = _context.Orderdetails.Where(x => x.OrderId == order.Id).ToList();
				for (int i = 0; i < orderDetails.Count; i++)
				{
                    Product p = _context.Products.Where(x => x.Id == orderDetails[i].ProductId).FirstOrDefault();
					p.Quantity += orderDetails[i].Quantity;
					_context.Products.Update(p);
				}

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
				return new ApiResponse<bool>()
				{
					Status = 200,
					Message = "Hủy đơn hàng thành công",
					Data = true
				};
			}
            catch(Exception )
            {
				return new ApiResponse<bool>()
				{
					Status = 500,
					Message = "Hủy đơn hàng thất bại",
					Data = false
				};
			}
        }

		//Nhân viên xác nhận đơn hàng Từ trạng thái chờ xác nhận(2) sang trạng thái giao hàng(3)
        public async Task<ApiResponse<bool>> ConfirmOrder(ConfirmOrderRequest request)
        {
            var employee = _context.Employees.Where(x => x.Id == request.EmployeeId).FirstOrDefault();
            if(employee == null)
            {
                return new ApiResponse<bool>()
                {
                    Status = 404,
                    Message = "Không tìm thấy nhân viên",
                    Data = false
                };
            }
            var order = _context.Orders.Where(x => x.Id == request.OrderId).FirstOrDefault();
            if (order==null)
            {
                return new ApiResponse<bool>()
                {
                    Status = 404,
                    Message = "Không tìm thấy đơn hàng",
                    Data = false
                };
            }
			if(order.Status == 0)
			{
				return new ApiResponse<bool>()
				{
					Status = 500,
					Message = "Đơn hàng đã bị hủy",
					Data = false
				};
			}
            // trừ số lượng sản phẩm
            //var orderDetails = _context.Orderdetails.Where(x => x.OrderId == order.Id).ToList();
			//Check xem có đủ số lượng sản phẩm không
			//for (int i = 0; i < orderDetails.Count; i++)
			//{
			//	Product p = _context.Products.Where(x => x.Id == orderDetails[i].ProductId).FirstOrDefault();

			//	if (p.Quantity < orderDetails[i].Quantity)
			//	{
			//		return new ApiResponse<bool>()
			//		{
			//			Status = 400,
			//			Message = "Không đủ sản phẩm để duyệt đơn hàng",
			//			Data = false
			//		};
			//	}
			//}
			// đã trừ khi thêm vào giỏ hàng
			//for (int i = 0; i < orderDetails.Count; i++)
			//         {
			//             Product p = _context.Products.Where(x => x.Id == orderDetails[i].ProductId).FirstOrDefault();
			//             p.Quantity -= orderDetails[i].Quantity;
			//             _context.Products.Update(p);
			//         }
			if (order.Status == 1) //Đơn hàng đã thanh toán , chờ nhân viên xác nhận
			{
                order.Employee = employee; //lưu nhân viên cập nhật các trạng thái của đơn hàng
                order.Status = 2;// Trạng thái đang giao hàng
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
			else // Trạng thái đơn hàng không tương thích
			{
                return new ApiResponse<bool>()
                {
                    Status = 500,
                    Message = "Trạng thái đơn hàng lỗi, kiểm tra lại",
                    Data = false
                };
            }
			

            return new ApiResponse<bool>()
            {
                Status = 200,
                Message = "Duyệt đơn hàng hàng thành công",
                Data = true
            };

        }
		//Tạo đơn hàng khách hàng sẻ tạo đơn hàng, đơn hàng tạo ra ở trạng thái chờ xác nhận(1)
		public async Task<ApiResponse<int>> CreateOrder(CreateOrderRequest request)
        {
            var cartDetails = _context.CartDetails.Where(x => x.CustomerId == request.CustomerId).ToList();
            if (cartDetails.IsNullOrEmpty())
            {
                return new ApiResponse<int>()
                {
                    Status = 404,
                    Message = "Không tìm thấy cart detail",
                    Data = 0
                };
            }
			// employee chỉ sử dụng để lưu thông tin xác nhận đơn hàng nên hiện tại sẽ để null
			//var employee = await _context.Employees.FindAsync(request.EmployeeId);
			//if (employee == null)
			//{
			//	return new ApiResponse<bool>()
			//	{
			//		Status = 404,
			//		Message = "Không tìm thấy employee",
			//		Data = false
			//	};
			//}
            var customer = _context.Customers.Where(x => x.Id == request.CustomerId).FirstOrDefault();
			if(customer == null)
            {
				return new ApiResponse<int>()
				{
					Status = 404,
					Message = "Không tìm thấy customer",
					Data = 0
				};
			}
			//Thanh toán online



			var order = new Order
            {
                CreateAt = DateTime.Now,
                CustomerId = request.CustomerId,
				//Tạo đơn hàng này sẻ có 
                Status = 1, // Trạng thái chờ thanh toán
                Customer = customer,
                //edit
                PaymentStatus = false,
				AddressShipping = request.AddressShipping
                //Employee = employee,
                //EmployeeId = employee.Id,
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
                    //edit
                    Price = p.Price * cartDetails[i].Quantity,
                    Order = newOrder,
                    Product = p
                };
                _context.Orderdetails.Add(o);
                newOrder.OrderDetails.Add(o);
                _context.CartDetails.Remove(cartDetails[i]);
            }
            _context.SaveChanges();

            return new ApiResponse<int>()
            {
                Status = 200,
                Message = "Đặt hàng thành công",
                Data = newOrder.Id
            };
        }

		//Sau khi đặt hàng xong sẻ trả về trang chọn phương thức thanh toán (Trả tiền khi nhận hàng , Thanh toán Online) ở phần đầu

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

		//Danh sách đơn hàng đã thanh toán chờ xác nhận
        public async Task<ApiResponse<List<OrderDTO>>> ListOrderNotConfirm()
        {
			try
			{
				var orders = _context.Orders.Where(x => x.Status == 1).ToList();
				var orderDtos = _mapper.Map<List<OrderDTO>>(orders);

				return new ApiResponse<List<OrderDTO>>()
				{
					Status = 200,
					Message = "Lấy danh sách Detail Order chưa xác nhận đã thành công",
					Data = orderDtos
				};
			}
			catch (Exception)
			{
				return new ApiResponse<List<OrderDTO>>()
				{
					Status = 500,
					Message = "Lấy danh sách Detail Order chưa xác nhận thất bại",
					Data = null
				};
			}
		}

		//Không thể sử dụng vì CartDetail có tới 2 khóa chính
		public async Task<ApiResponse<bool>> CreateOrderByListId(CreateOrderByListId request)
		{
            try
            {
				var employee = await _context.Employees.FindAsync(request.EmployeeId);
				if (employee == null)
				{
					return new ApiResponse<bool>()
					{
						Status = 404,
						Message = "Không tìm thấy employee",
						Data = false
					};
				}
				var customer = _context.Customers.Where(x => x.Id == request.CustomerId).FirstOrDefault();
				if (customer == null)
				{
					return new ApiResponse<bool>()
					{
						Status = 404,
						Message = "Không tìm thấy customer",
						Data = false
					};
				}
				var order = new Order
				{
					CreateAt = DateTime.Now,
					CustomerId = request.CustomerId,
					Status = 1,
					Customer = customer,
					//edit
					PaymentStatus = false,
					Employee = employee,
					EmployeeId = employee.Id,
				};
				_context.Add(order);
				_context.SaveChanges();
				var newOrder = _context.Orders.OrderByDescending(x => x.CreateAt).FirstOrDefault();

				foreach (int i in request.Ids)
				{
					var cartDetail = _context.CartDetails.FirstOrDefault(x => x.Customer == customer);
					Product p = _context.Products.Where(x => x.Id == cartDetail.ProductId).FirstOrDefault();
					OrderDetail o = new OrderDetail
					{
						OrderId = newOrder.Id,
						ProductId = cartDetail.ProductId,
						Quantity = cartDetail.Quantity,
						//edit
						Price = p.Price * cartDetail.Quantity,
						Order = newOrder,
						Product = p
					};
					_context.Orderdetails.Add(o);
					newOrder.OrderDetails.Add(o);
					_context.CartDetails.Remove(cartDetail);
				}
				_context.SaveChanges();

				return new ApiResponse<bool>()
				{
					Status = 200,
					Message = "Đặt hàng thành công",
					Data = true
				};
			}
            catch(Exception ex)
            {
				return new ApiResponse<bool>()
				{
					Status = 500,
					Message = "Đặt hàng thất bại",
					Data = false
				};
			}
		}

		//Nhân Viên Xóa đơn Hàng khi đã xong
		public async Task<ApiResponse<bool>> DeleteOrder(int orderId)
		{
			try
			{
				var order = await _context.Orders.FindAsync(orderId);
				if (order == null)
				{

				}	
				_context.Orders.Remove(order);
				await _context.SaveChangesAsync();
				return new ApiResponse<bool>()
				{
					Status = 200,
					Message = "Xóa đơn hàng thành công",
					Data = true
				};
			}
			catch (Exception)
			{
				return new ApiResponse<bool>()
				{
					Status = 500,
					Message = "Xóa đơn hàng thất bại",
					Data = false
				};
			}
		}
		//Lấy tất cả các Order để nhân viên quản lý
        public async Task<ApiResponse<List<OrderDTO>>> ListAllOrders()
        {
            try
            {
                var orders = _context.Orders.ToList();
                var orderDtos = _mapper.Map<List<OrderDTO>>(orders);

                return new ApiResponse<List<OrderDTO>>()
                {
                    Status = 200,
                    Message = "Lấy danh sách Order thành công",
                    Data = orderDtos
                };
            }
            catch (Exception)
            {
                return new ApiResponse<List<OrderDTO>>()
                {
                    Status = 500,
                    Message = "Lấy danh sách Order thất bại",
                    Data = null
                };
            }
        }
    }
}
