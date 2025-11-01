using Microsoft.AspNetCore.Http;

namespace Newspaper.Application.DTOs.News.Request;

public class UpdateNewsDTO
{
    public int NewsId { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Content { get; set; }
    public IFormFile Image { get; set; }
}