using MediatR;
using Newspaper.Application.DTOs.News.Request;
using Newspaper.Application.Helpers;
using Newspaper.Domain.Entities;
using Newspaper.Domain.Enums;
using Newspaper.Domain.Interfaces.Repositories;
using Newspaper.Domain.Interfaces.Services;

namespace Newspaper.Application.CQRS.Commands.News;

public record CreateNewsCommand(string UserId, CreateNewsDTO Model) : IRequest;

public class CreateNewsCommandHandler(ITranslatorService translatorService, IImageService imageService, INewsRepository newsRepository) : IRequestHandler<CreateNewsCommand>
{
    public async Task Handle(CreateNewsCommand request, CancellationToken cancellationToken)
    {
        var joinedLine = TextParser.JoinText(new[] {request.Model.Title, request.Model.SubTitle, request.Model.Content});
        
        (string russianText, string englishText) = await translatorService.TranslateToRussianAndEnglishAsync(joinedLine);
        
        var englishTranslation = TextParser.ParseText(englishText);
        englishTranslation.Language = Languages.English;
        
        var russianTranslation = TextParser.ParseText(russianText);
        russianTranslation.Language = Languages.Russian;

        var imageUrl = await imageService.UploadAsync(request.Model.Image);

        var news = new Domain.Entities.News()
        {
            ImageUrl = imageUrl,
            UserId = request.Model.UserId,
            Translations = new List<NewsTranslations> { russianTranslation, englishTranslation }
        };

        await newsRepository.AddAsync(news, cancellationToken);
    }
}