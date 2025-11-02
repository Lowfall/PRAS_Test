namespace Newspaper.Application.DTOs.News.Request;

public class NewsDTO
{
    public string ImageUrl { get; set; }
    public ICollection<TranslationDTO> Translations { get; set; }
}