using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.Product;

namespace Kitchen_Appliances_MVC.ApiServices
{
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public const string BaseUrl = "/gateway/product";

        //Đã test

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<APIResponse<bool>> CreateProduct(CreateProductRequest request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseUrl, request);

            APIResponse<bool> result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
            return result;
        }

        public async Task<APIResponse<bool>> DeleteProduct(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(BaseUrl + $"/{id}");
            APIResponse<bool> result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
            return result;
        }

        public async Task<APIResponse<List<ProductDTO>>> GetAllProducts()
        {
            return await _httpClient.GetFromJsonAsync<APIResponse<List<ProductDTO>>>(BaseUrl);
        }

        public async Task<APIResponse<ProductDTO>> GetProductById(int id)
        {
            return await _httpClient.GetFromJsonAsync<APIResponse<ProductDTO>>(BaseUrl + $"/{id}");
        }

        public async Task<APIResponse<List<ProductDTO>>> ListProductByCategory(int categoryId)
        {
            return await _httpClient.GetFromJsonAsync<APIResponse<List<ProductDTO>>>(BaseUrl + $"/category/{categoryId}");
        }

        public async Task<APIResponse<bool>> UpdateProduct(int id, UpdateProductRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(BaseUrl + $"/{id}", request);

            APIResponse<bool> result = await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
            return result;
        }
    }
}
