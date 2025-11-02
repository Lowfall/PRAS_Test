using Microsoft.AspNetCore.Http;

namespace Newspaper.Application.DTOs.News.Request;

public class CreateNewsDTO
{
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Content { get; set; }
    public string UserId { get; set; }
    public IFormFile Image { get; set; }
}