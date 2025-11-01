using Mapster;
using MediatR;
using Newspaper.Application.DTOs.News.Request;
using Newspaper.Application.Exceptions;
using Newspaper.Application.Helpers;
using Newspaper.Domain.Entities;
using Newspaper.Domain.Enums;
using Newspaper.Domain.Interfaces.Repositories;
using Newspaper.Domain.Interfaces.Services;

namespace Newspaper.Application.CQRS.Commands.News;

public record UpdateNewsCommand( UpdateNewsDTO Model): IRequest;

public class UpdateNewsCommandHandler(
    INewsRepository newsRepository, 
    IImageService imageService, 
    ITranslatorService translatorService) : IRequestHandler<UpdateNewsCommand>
{
    public async Task Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
    {
        var news = await newsRepository.GetNewsWithTranslationsAsync(request.Model.NewsId, cancellationToken);

        if (news is null)
        {
            throw new NotFoundException("There is no news with such id");
        }
        
        var joinedLine = TextParser.JoinText(new[] {request.Model.Title, request.Model.SubTitle, request.Model.Content});
        
        (string russianText, string englishText) = await translatorService.TranslateToRussianAndEnglishAsync(joinedLine);
        
        var englishTranslation = TextParser.ParseText(englishText);
        englishTranslation.Language = Languages.English;
        
        var russianTranslation = TextParser.ParseText(russianText);
        russianTranslation.Language = Languages.Russian;

        List<NewsTranslations> translationList =  new List<NewsTranslations>() {russianTranslation, englishTranslation};
        
        var newNews = new NewsDTO()
        { 
            Translations = translationList.Adapt<List<TranslationDTO>>()
        };
        
        if (request.Model.Image is not null)
        {
            var imageUrl = await imageService.UploadAsync(request.Model.Image);
            await imageService.DeleteAsync(news.ImageUrl);
            newNews.ImageUrl = imageUrl;
        }

        newNews.Adapt(news);
        
        await newsRepository.UpdateAsync(news, cancellationToken);
    }
}