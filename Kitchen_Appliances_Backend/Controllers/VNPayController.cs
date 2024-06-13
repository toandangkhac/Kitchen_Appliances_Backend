using Kitchen_Appliances_Backend.Commons.Exceptions;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.PaymentService.VnPay;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        private readonly IVnPayService _vpnPayService;
        private readonly IBillRepository _billRepository;
        private readonly DataContext _context;

        public VnPayController(IVnPayService vpnPayService , DataContext context, IBillRepository billRepository)
        {
            _billRepository = billRepository;
            _vpnPayService = vpnPayService;
            _context = context;
        }
        
        [HttpGet("{orderId}")]
        public async Task<IActionResult> CreatePaymentUrl(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                return Ok(new ApiResponse<string>()
                {
                    Status = 500,
                    Message = "Không tìm thấy đơn hàng",
                    Data = null
                });
            }

            //edit
            decimal total = 0;
            var orderDetails = _context.Orderdetails.Where(x => x.OrderId == order.Id).ToList();
            if(orderDetails.Count == 0)
            {
                return Ok( new ApiResponse<string>()
                {
                    Status = 500,
                    Message = "Đơn hàng rỗng, không thể thanh toán",
                    Data = null
                });
            }    
            foreach (var orderDetail in orderDetails)
            {
                total += orderDetail.Price;
            }
            
            var customer = _context.Customers.Where(x => x.Id == order.CustomerId).FirstOrDefault();
            if(customer == null)
            {
                throw new NotFoundException("Không tìm thấy người dùng");
            }

            VnPaymentRequestModel model = new VnPaymentRequestModel()
            {
                OrderId = orderId.ToString(),
                FullName = customer.Fullname,
                Description = "Thanh Toán Đơn Hàng",
                //Loại bỏ
                Amount = Math.Floor(total)
            };
            string res = _vpnPayService.CreatePaymentUrl(HttpContext, model);
            //string res = "Hihi";
            var response = new ApiResponse<string>()
            {
                Status = 200,
                Message = "Chuyển đến trang thanh toán VNPAY",
                Data = res
            };
            return Ok(response);
        }
        //Giao dịch thành công thì load về trang này cập nhật trạng thái đơn hàng và tạo bill
        [HttpGet("call-back")]
        public async Task<IActionResult> PaymentCallBack()
        {
            try
            {
                var response = _vpnPayService.PaymentExecute(Request.Query);
                if (response == null || response.VnPayResponseCode != "00")
                {
                    //roll back order , rollback số lượng sản phẩm
                    //return Ok(new ApiResponse<VnPaymentResponseModel>()
                    //{
                    //    Status = 500,
                    //    Message = "Thanh toán bị lỗi",
                    //    Data = null
                    //});
                }
                else
                {
                    //Cập nhật trạng thái đơn hàng từ chờ thanh toán sang chờ giao hàng
                    var order = _context.Orders.Find(int.Parse(response.OrderId));
                    order.PaymentStatus = true;
                    order.Status = 1;
                    //Tạo bill hóa đơn
                    var bill = await _billRepository.savePaymentInfor(order.Id);
                    if (bill.Status != 200)
                    {
                        throw new InvalidRequestException("Không thể tạo hóa đơn");
                    }
                    _context.Orders.Update(order);
                    _context.SaveChanges();
                }
                //return Ok(new ApiResponse<VnPaymentResponseModel>()
                //{
                //    Status = 200,
                //    Message = "Thanh toán thành công",
                //    Data = response
                //});
                return Redirect("https://localhost:7260/VNPay/ThanhToanThanhCong");
            }
            catch(Exception )
            {
                return Ok(new ApiResponse<VnPaymentResponseModel>()
                {
                    Status = 500,
                    Message = "Thanh toán bị lỗi",
                    Data = null
                });
            }

        }
    }
}
