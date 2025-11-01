using Newspaper.Domain.Entities;

namespace Newspaper.Application.Helpers;

public static class TextParser
{
    private const string SEPARATOR = "    ";

    public static string JoinText(IEnumerable<string> texts)
    {
        return String.Join(SEPARATOR, texts);
    }
    
    public static NewsTranslations ParseText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException("Text cannot be empty");
        }
        
        var parts = text.Split(SEPARATOR);

        if (parts.Length < 3)
        {
            throw new ArgumentException("Text must have at least three parts");
        }

        var translation = new NewsTranslations()
        {
            Title = parts[0],
            Subtitle = parts[1],
            Content = parts[2]
        };
        
        return translation;
    }
    
}