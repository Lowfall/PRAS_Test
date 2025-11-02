using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newspaper.Domain.Interfaces.Services;
using Newspaper.Infrastructure.Options;

namespace Newspaper.Infrastructure.Services;

public class ImageService : IImageService
{
    private readonly Cloudinary _cloudinary;
    
    public ImageService(IOptions<ExternalServicesOptions> options)
    {
        var account = new Account()
        {
            Cloud = options.Value.CloudinaryOptions.CloudName,
            ApiKey = options.Value.CloudinaryOptions.ApiKey,
            ApiSecret = options.Value.CloudinaryOptions.ApiSecret,
        };
        
        _cloudinary = new Cloudinary(account);
    }
    
    public async Task<string> UploadAsync(IFormFile file)
    {
        using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation()
                .Width(800)                
                .Height(450)              
                .Crop("fill")              
                .Gravity("auto")           
                .FetchFormat("auto")       
                .Quality("auto"),
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
        {
            throw new Exception(uploadResult.Error.Message);
        }

        return uploadResult.SecureUri.AbsoluteUri;
    }

    public async Task DeleteAsync(string imageUrl)
    {
        var publicId = imageUrl.Split("/").LastOrDefault().Split(".").FirstOrDefault();
        var deleteParams = new DeletionParams(publicId);
        await _cloudinary.DestroyAsync(deleteParams);
    }
}