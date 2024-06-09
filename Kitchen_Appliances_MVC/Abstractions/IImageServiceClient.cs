using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.ViewModels.Image;

namespace Kitchen_Appliances_MVC.Abstractions
{
	public interface IImageServiceClient
	{
		Task<APIResponse<ImageDTO>> GetImageById(int id);

		Task<APIResponse<bool>> CreateImage(CreateImageRequest request);

		Task<APIResponse<bool>> UpdateImage(int id, UpdateImageRequest request);

		Task<APIResponse<bool>> SetImageDefault(int id);
		Task<APIResponse<bool>> DeleteImage(int id);

		Task<APIResponse<List<ImageDTO>>> GetAllImagesByProduct(int productId);
	}
}
