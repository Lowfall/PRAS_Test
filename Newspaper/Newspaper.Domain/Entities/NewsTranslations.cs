using Newspaper.Domain.Enums;

namespace Newspaper.Domain.Entities;

public class NewsTranslations
{
    public int Id { get; set; }
    public Languages Language { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Content { get; set; }
    
    public int NewsId { get; set; }
    public News News { get; set; }
}