using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Exceptions;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Image;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;
using Kitchen_Appliances_Backend.Services;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _upload;

        public ImageRepository(DataContext context, IMapper mapper, IUploadService upload)
        {
            _context = context;
            _mapper = mapper;
            _upload = upload;
        }

        public async Task<ApiResponse<bool>> CreateImage(CreateImageRequest request)
        {
            try
            {
                var product = await _context.Products.FindAsync(request.ProductId);
                if(product == null)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 404,
                        Message = "Không tìm thấy product",
                        Data = false
                    };
                }
                var image = new Image()
                {
                    ProductId = request.ProductId,
                    Product = product,
                };
                
                if(request.Url != null)
                {
                    image.Url = await _upload.UploadFile(request.Url);
                }

                _context.Images.Add(image);
                _context.SaveChanges();

                return new ApiResponse<bool>()
                {
                    Status = 200,
                    Message = "Thêm product image thành công",
                    Data = true
                };
            }
            catch (Exception)
            {
                return new ApiResponse<bool>()
                {
                    Status = 400,
                    Message = "Thêm product image thất bại",
                    Data = false
                };
            }
        }

        public async Task<ApiResponse<bool>> DeleteImage(int id)
        {
            try
            {
                var image = _context.Images.Find(id);
                if (image == null)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 404,
                        Message = "Không tìm thấy image",
                        Data = false
                    };
                }
                await _upload.DeleteFile(image.Url);
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();

                return new ApiResponse<bool>()
                {
                    Status = 200,
                    Message = "Xóa image thành công",
                    Data = true
                };
            }
            catch(Exception )
            {
                return new ApiResponse<bool>()
                {
                    Status = 404,
                    Message = "Xóa image thất bại",
                    Data = false
                };
            }
        }

        public async Task<ApiResponse<List<ImageDTO>>> GetAllImages(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return new ApiResponse<List<ImageDTO>>()
                {
                    Status = 404,
                    Message = "Không tìm thấy product",
                    Data = null
                };
            }
            var images = _context.Images.Where(x => x.ProductId == productId).ToList();

            var imageDtos = _mapper.Map<List<ImageDTO>>(images);

            return new ApiResponse<List<ImageDTO>>()
            {
                Status = 200,
                Message = "Lấy danh sách thành công",
                Data = imageDtos
            };
        }

        public async Task<ApiResponse<ImageDTO>> GetImageById(int id)
        {
            try
            {
                var image = await _context.Images.FindAsync(id);
                if (image == null)
                {
                    return new ApiResponse<ImageDTO>()
                    {
                        Status = 404,
                        Message = "Không tìm thấy image",
                        Data = null
                    };
                }
                
                return new ApiResponse<ImageDTO>()
                {
                    Status = 200,
                    Message = "Get image product",
                    Data = _mapper.Map<ImageDTO>(image)
                };
            }
            catch (Exception)
            {
                return new ApiResponse<ImageDTO>()
                {
                    Status =400,
                    Message = "Get image bị lỗi",
                    Data = null
                };
            }
        }

        public Task<ApiResponse<bool>> SetImageDefault(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<bool>> UpdateImage(int id, UpdateImageRequest request)
        {
            try
            {
                var image = _context.Images.FirstOrDefault(x => x.Id == id);
                if (image == null)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = 200,
                        Message = "Không tìm thấy image",
                        Data = false
                    };
                }
                if (request.Url != null)
                {
                    image.Url = await _upload.UploadFile(request.Url);
                }
                await _upload.DeleteFile(image.Url);
                _context.Images.Update(image);
                await _context.SaveChangesAsync();
                return new ApiResponse<bool>()
                {
                    Status = 404,
                    Message = "Update image thành công",
                    Data = true
                };
            }
            catch (Exception)
            {
                return new ApiResponse<bool>()
                {
                    Status = 404,
                    Message = "Update image thành công",
                    Data = true
                };
            }
        }
    }
}
