namespace Newspaper.Domain.Entities;

public class News
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    
    
    public string UserId { get; set; }
    public User User { get; set; }
    public ICollection<NewsTranslations> Translations { get; set; }
}