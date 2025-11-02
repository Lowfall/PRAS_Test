using Newspaper.Domain.Enums;

namespace Newspaper.Application.DTOs.News.Request;

public class TranslationDTO
{
    public Languages Language { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Content { get; set; }
}