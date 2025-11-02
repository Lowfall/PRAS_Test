using MediatR;
using Newspaper.Application.Exceptions;
using Newspaper.Domain.Interfaces.Repositories;

namespace Newspaper.Application.CQRS.Queries.News;

public record GetNewsArticleQuery(int NewsId) : IRequest<Domain.Entities.News>;

public class GetNewsArticleQueryHandler(INewsRepository newsRepository) : IRequestHandler<GetNewsArticleQuery, Domain.Entities.News>
{
    public async Task<Domain.Entities.News> Handle(GetNewsArticleQuery request, CancellationToken cancellationToken)
    {
        var newsArticle = await newsRepository.GetNewsWithTranslationsAsync(request.NewsId, cancellationToken);

        if (newsArticle is null)
        {
            throw new NotFoundException("There is no news with such id");
        }
        
        return newsArticle;
    }
}