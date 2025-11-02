using MediatR;
using Newspaper.Domain.Interfaces.Repositories;

namespace Newspaper.Application.CQRS.Queries.News;

public record GetLatestNewsQuery(int Amount) : IRequest<List<Domain.Entities.News>>;

public class GetLatestNewsQueryHandler(INewsRepository newsRepository) : IRequestHandler<GetLatestNewsQuery, List<Domain.Entities.News>>
{
    public async Task<List<Domain.Entities.News>> Handle(GetLatestNewsQuery request, CancellationToken cancellationToken)
    {
        var latestNews = await newsRepository.GetNLatestNewsAsync(request.Amount, cancellationToken);
        
        return latestNews;
    }
}