namespace Newspaper.Web.Models.Home;

public class NewsViewModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<TranslationViewModel> Translations{ get; set; }
}