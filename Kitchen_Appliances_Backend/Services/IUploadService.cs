namespace Kitchen_Appliances_Backend.Services
{
    public interface IUploadService
    {
        Task<string> UploadFile(IFormFile file);

        Task DeleteFile(string url);
    }
}
