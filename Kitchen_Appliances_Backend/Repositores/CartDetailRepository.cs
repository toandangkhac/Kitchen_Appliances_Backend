using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Exceptions;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.CartItem;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;
using Kitchen_Appliances_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class CartDetailRepository : ICartDetailRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        //private string UserId;

        private static string _CartDetail = "CartDetail Repository";

        public CartDetailRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            //_currentUserService = currentUserService;
            //UserId = _currentUserService.UserName;
        }
        // vì đã ngăn chặn list product có chức status false
        public async Task<ApiResponse<bool>> AddCartDetailToCart(CreateCartDetailRequest request)
        {
            try
            {
                var product = await _context.Products.FindAsync(request.ProductId);
                if (product == null)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 404,
                        Message = "Không tìm thấy product",
                        Data = false
                    };
                }
                if (product.Quantity < request.Quantity)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 400,
                        Message = "Số lượng yêu cầu vượt quá",
                        Data = false
                    };
                }

                var customer = _context.Customers.FirstOrDefault(x => x.Id == request.CustomerId);
                if (customer == null)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 404,
                        Message = "Không tìm thấy người dùng",
                        Data = false
                    };
                }

                var cartDetail = _context.CartDetails.FirstOrDefault(x => x.Product == product && x.Customer == customer);
                if (cartDetail == null)
                {
                    cartDetail = new CartDetail()
                    {
                        ProductId = product.Id,
                        Product = product,
                        Quantity = request.Quantity,
                        Customer = customer,
                        CustomerId = customer.Id
                    };
                    _context.CartDetails.Add(cartDetail);
                }
                else
                {
                    cartDetail.Quantity += request.Quantity;
                    _context.CartDetails.Update(cartDetail);
                }
               
                product.Quantity -= request.Quantity;
                _context.Products.Update(product);
                
                await _context.SaveChangesAsync();
                return new ApiResponse<bool>()
                {
                    Status = 200,
                    Message = "Thêm giỏ hàng thành công",
                    Data = true
                }; ;
            }
            catch (Exception )
            {
                return new ApiResponse<bool>()
                {
                    Status = 400,
                    Message = "Thêm giỏ hàng thất bại",
                    Data = false
                };
            }
        }
        public async Task<ApiResponse<bool>> DeleteCartDetail(GetCartDetailRequest request)
        {
            try
            {
                var product = await _context.Products.FindAsync(request.ProductId);
                if (product == null)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 404,
                        Message = "Không tìm thấy product",
                        Data = false
                    };
                }
                var customer = _context.Customers.FirstOrDefault(x => x.Id == request.CustomerId);
                if (customer == null)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 404,
                        Message = "Không tìm thấy người dùng",
                        Data = false
                    };
                }

                var cartDetail = _context.CartDetails
                   .FirstOrDefault(x => x.Product == product && x.Customer == customer);
                if (cartDetail == null)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 404,
                        Message = "Không tìm thấy cartdetail",
                        Data = false
                    };
                }
                product.Quantity += cartDetail.Quantity;
                _context.Products.Update(product);
                _context.CartDetails.Remove(cartDetail);
                await _context.SaveChangesAsync();
                return new ApiResponse<bool>()
                {
                    Status = 200,
                    Message = "Xóa CartDetail thành công",
                    Data= true
                };
            }
            catch (Exception )
            {
                return new ApiResponse<bool>()
                {
                    Status = 400,
                    Message = "Xóa CartDetail bị lỗi, vui lòng thử lại",
                    Data = false
                };
            }   
        }

        public async Task<ApiResponse<CartDetailDTO>> GetCartDetail(GetCartDetailRequest request)
        {
            var product = _context.Products.Find(request.ProductId);
            if(product == null)
            {
                return new ApiResponse<CartDetailDTO>()
                {
                    Status = 404,
                    Message = "Không tìm thấy product",
                    Data = null
                };
            }

            var customer = _context.Customers.Find(request.CustomerId);
            if (customer == null)
            {
                return new ApiResponse<CartDetailDTO>()
                {
                    Status = 404,
                    Message = "Không tìm thấy customer",
                    Data = null
                };
            }

            var cartDetail = _context.CartDetails.FirstOrDefault(x => x.Product == product && x.Customer == customer);
            if (cartDetail == null)
            {
                return new ApiResponse<CartDetailDTO>()
                {
                    Status = 404,
                    Message = "Không tìm thấy cartdetail",
                    Data = null
                };
            }
            var cartDetailDto = _mapper.Map<CartDetailDTO>(cartDetail);

            return new ApiResponse<CartDetailDTO>()
            {
                Status = 200,
                Message = "Lấy CartItem thành công",
                Data = cartDetailDto
            };
        } 

        public async Task<ApiResponse<List<CartDetailDTO>>> GetCartDetailByCustomer(int customerId)
        {
            try
            {
                var customer =  _context.Customers.Find(customerId);
                if (customer == null)
                {
                    return new ApiResponse<List<CartDetailDTO>>()
                    {
                        Status = 404,
                        Message = "Không tìm thấy customer",
                        Data = new List<CartDetailDTO>()
                    };
                }
                var cartDtos = new List<CartDetailDTO>();
                var cartDetails = _context.CartDetails.Where(x => x.CustomerId == customer.Id).ToList();
                cartDetails.ForEach(x => cartDtos.Add(_mapper.Map<CartDetailDTO>(x)));
                return new ApiResponse<List<CartDetailDTO>>()
                {
                    Status = 200,
                    Message = "Lấy danh sách CardDetail thành công",
                    Data = cartDtos
                };
            }
            catch (Exception )
            {
                return new ApiResponse<List<CartDetailDTO>>()
                {
                    Status = 400,
                    Message = "Lấy danh sách bị lỗi",
                    Data = null
                };
            }
        }

        public Task<ApiResponse<long>> UpdateCartItemQuantity(UpdateCartDetailRequest request)
        {
            
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> DeleteListCartDetail(List<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
