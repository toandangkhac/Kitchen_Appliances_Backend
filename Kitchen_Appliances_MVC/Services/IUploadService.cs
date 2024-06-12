namespace Kitchen_Appliances_MVC.Services
{
	public interface IUploadService
	{
		Task<string> UploadFile(IFormFile file);

		Task DeleteFile(string url);
	}
}
