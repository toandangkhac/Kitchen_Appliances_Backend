using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Image;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IImageRepository
    {
        Task<ApiResponse<ImageDTO>> GetImageById(int id);

        Task<ApiResponse<bool>> CreateImage(CreateImageRequest request);
		//Task<ApiResponse<bool>> CreateImage(ImageDTO request);

		Task<ApiResponse<bool>> UpdateImage(int id, UpdateImageRequest request);
        
        Task<ApiResponse<bool>> SetImageDefault(int id);

        Task<ApiResponse<bool>> DeleteImage(int id);

        Task<ApiResponse<List<ImageDTO>>> GetAllImages(int productId);
    }
}
