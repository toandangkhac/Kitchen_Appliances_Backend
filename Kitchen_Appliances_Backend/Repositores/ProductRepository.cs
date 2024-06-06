using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Product;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class ProductRepository: IProductRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public ProductRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> CreateProduct(CreateProductRequest request)
        {
            try
            {
                var product = _mapper.Map<Product>(request);
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return new ApiResponse<bool>()
                {
                    Status = 200,
                    Message = "Tạo product thành công",
                    Data = true
                };
            }
            catch (Exception)
            {
                return new ApiResponse<bool>()
                {
                    Status = 400,
                    Message = "Tạo product lỗi, Vui lòng kiểm tra lại",
                    Data = false
                };
            }
        }

        public async Task<ApiResponse<bool>> DeleteProduct(int id)
        {
            try
            {
               var product = await _context.Products.FindAsync(id);
               if (product == null)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 404,
                        Message = "Không tìm thấy product",
                        Data = false
                    };
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return new ApiResponse<bool>() { Status = 200, Message = "Xóa product thành công", Data = true};
            }
            catch (Exception)
            {
                return new ApiResponse<bool>()
                {
                    Status = 400,
                    Message = "Xóa product bị lỗi",
                    Data = false
                };
            }
        }

        public async Task<ApiResponse<List<ProductDTO>>> GetAllProducts()
        {
            try
            {
                var products = _context.Products.Where(x => x.Status == true).ToList();
                var productDtos = new List<ProductDTO>();
                products.ForEach(x => productDtos.Add(_mapper.Map<ProductDTO>(x)));
                return new ApiResponse<List<ProductDTO>>()
                {
                    Status = 200,
                    Message = "Lấy list product thành công",
                    Data = productDtos
                };
            }
            catch (Exception)
            {
                return new ApiResponse<List<ProductDTO>>()
                {
                    Status = 400,
                    Message = "Lấy list product bị lỗi",
                    Data = new List<ProductDTO>()
                };
            }
        }

        public async Task<ApiResponse<ProductDTO>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiResponse<ProductDTO>()
                {
                    Status = 404,
                    Message = "Không tìm thấy product",
                    Data = null
                };
            }
            return new ApiResponse<ProductDTO>()
            {
                Status = 200,
                Message = "Lấy list product thành công",
                Data = _mapper.Map<ProductDTO>(product)
            };
        }

        public async Task<ApiResponse<List<ProductDTO>>> ListProductByCategory(int categoryId)
        {
            var products = _context.Products.Where(x => x.CategoryId == categoryId).ToList();
            var productDtos = new List<ProductDTO>();
            products.ForEach(x => productDtos.Add(_mapper.Map<ProductDTO>(x)));
            return new ApiResponse<List<ProductDTO>>()
            {
                Status = 200,
                Message = "Lấy list product thành công",
                Data = productDtos
            };
        }

        public async Task<ApiResponse<bool>> UpdateProduct(int id, UpdateProductRequest request)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 404,
                        Message = "Không tìm thấy product",
                        Data = false
                    };
                }

                //check mapper
                product = _mapper.Map<Product>(request);
                if(request.Name != null)
                {
                    product.Name = request.Name;
                }    
                else if(request.Description != null)
                {
                    product.Description = request.Description;
                }
                else if(request.Price != null)
                {
                    product.Price = request.Price;
                }
                else if(request.Quantity != null)
                {
                    product.Quantity += request.Quantity;
                }    

                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return new ApiResponse<bool>()
                {
                    Status = 200,
                    Message = "Update product thành công",
                    Data = true
                };
            }
            catch (Exception)
            {
                return new ApiResponse<bool>()
                {
                    Status = 400,
                    Message = "Update product bị lỗi, kiểm tra lại",
                    Data = false
                };
            }

        }
    }
}
