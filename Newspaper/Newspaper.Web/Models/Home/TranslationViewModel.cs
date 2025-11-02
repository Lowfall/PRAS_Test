using Newspaper.Domain.Enums;

namespace Newspaper.Web.Models.Home;

public class TranslationViewModel
{
    public Languages Language { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Content { get; set; }
}