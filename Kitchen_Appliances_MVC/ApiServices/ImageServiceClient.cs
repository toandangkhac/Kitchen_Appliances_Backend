using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.Image;
using Kitchen_Appliances_MVC.ViewModels.Product;
using Microsoft.CodeAnalysis;

namespace Kitchen_Appliances_MVC.ApiServices
{
	public class ImageServiceClient : IImageServiceClient
	{
		private readonly HttpClient _httpClient;
		private const string BaseUrl = "/gateway/image";
		public ImageServiceClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		
		// đã test phần get listimage, getbyid, deletebyId
		//phần tạo và update chưa tìm ra giải pháp

		public Task<APIResponse<bool>> CreateImage(CreateImageRequest request)
		{
			throw new NotImplementedException();
		}

		public async Task<APIResponse<bool>> DeleteImage(int id)
		{
			HttpResponseMessage response = await _httpClient.DeleteAsync(BaseUrl + $"/{id}");
			APIResponse<bool> result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
			return result;
		}

		public async Task<APIResponse<List<ImageDTO>>> GetAllImagesByProduct(int productId)
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<List<ImageDTO>>>(BaseUrl + $"/product/{productId}");
		}

		public async Task<APIResponse<ImageDTO>> GetImageById(int id)
		{
			return await _httpClient.GetFromJsonAsync<APIResponse<ImageDTO>>(BaseUrl + $"/{id}");
		}

		public Task<APIResponse<bool>> SetImageDefault(int id)
		{
			throw new NotImplementedException();
		}

		public Task<APIResponse<bool>> UpdateImage(int id, UpdateImageRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
