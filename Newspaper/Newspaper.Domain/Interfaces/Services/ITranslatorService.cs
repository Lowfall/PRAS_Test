namespace Newspaper.Domain.Interfaces.Services;

public interface ITranslatorService
{
    Task<(string Russian, string English)> TranslateToRussianAndEnglishAsync(string text);
}