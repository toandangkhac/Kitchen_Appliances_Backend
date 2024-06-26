﻿
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Kitchen_Appliances_Backend.DependencyInjection.Options;

namespace Kitchen_Appliances_Backend.Services.ServiceImpl
{
    public class UploadService : IUploadService
    {
        private readonly Cloudinary _cloudinary;

        public UploadService(IConfiguration configuration)
        {
            CloudinaryOptions cloudinaryOptions = new CloudinaryOptions();
            configuration.GetSection(nameof(CloudinaryOptions)).Bind(cloudinaryOptions);
            var account = new Account(cloudinaryOptions.CloudName, cloudinaryOptions.APIKey, cloudinaryOptions.APISecret);
            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        private static string GetPublicId(string url)
        {
            return Path.GetFileNameWithoutExtension(url);
        }
        public async Task DeleteFile(string url)
        {
            var publicId = GetPublicId(url);
            await _cloudinary.DestroyAsync(new(publicId));
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            if (file == null) return "";

            var uploadParams = new RawUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.Url.AbsoluteUri;
        }
    }
}
