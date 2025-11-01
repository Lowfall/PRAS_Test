using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Newspaper.Domain.Interfaces.Services;
using Newspaper.Infrastructure.Options;

namespace Newspaper.Infrastructure.Services;

public record TranslationResponse(OutputItem[] Outputs);
public record OutputItem(string Output);

public class TranslatorService(
    HttpClient httpClient, 
    IOptions<ExternalServicesOptions> options): ITranslatorService
{
    private const string RUSSIAN_ABBREVIATION = "ru";
    private const string ENGLISH_ABBREVIATION = "en";
    private const string SOURCE_LANGUAGE = "auto";
    private const string TRANSLATHION_PATH = "translation/text/translate";
    private readonly string API_KEY = options.Value.TranslatorOptions.ApiKey; 
    
    //There could be error depending on the duration of API subscription , but I hope trial period is enough for test case 
    
    public async Task<(string Russian, string English)> TranslateToRussianAndEnglishAsync(string text)
    {
        string russian = await TranslateAsync(text, RUSSIAN_ABBREVIATION);
        string english = await TranslateAsync(text, ENGLISH_ABBREVIATION);
        
        return (russian, english);
    }

    private async Task<string> TranslateAsync(string input, string target)
    {
        var source = target == RUSSIAN_ABBREVIATION ? ENGLISH_ABBREVIATION : RUSSIAN_ABBREVIATION;
        var url = TRANSLATHION_PATH + $"?key={API_KEY}&source={source}&target={target}&input={input}";
        
        var response = await httpClient.PostAsync(url,null);
        
        if (!response.IsSuccessStatusCode)
            return input;

        var content = await response.Content.ReadFromJsonAsync<TranslationResponse>();

        return content.Outputs[0].Output;
    }

}