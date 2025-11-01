using Microsoft.AspNetCore.Http;

namespace Newspaper.Domain.Interfaces.Services;

public interface IImageService
{
    Task<string> UploadAsync(IFormFile file);
    Task DeleteAsync(string imageUrl);
}