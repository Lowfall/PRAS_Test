using MediatR;
using Newspaper.Domain.Interfaces.Repositories;

namespace Newspaper.Application.CQRS.Queries.News;

public record GetPaginatedNewsQuery(int PageNumber, int Limit) : IRequest<(int amount, List<Domain.Entities.News> newsList)>;

public class GetPaginatedNewsQueryHandler(INewsRepository newsRepository) :  IRequestHandler<GetPaginatedNewsQuery, (int amount,List<Domain.Entities.News> newsList)>
{
    public async Task<(int amount,List<Domain.Entities.News> newsList)> Handle(GetPaginatedNewsQuery request, CancellationToken cancellationToken)
    {
        var news = await newsRepository.GetPaginatedNewsAsync(request.PageNumber, request.Limit, cancellationToken);
        
        var newsAmount = await newsRepository.GetAmountAsync();
        
        return (newsAmount,news);
    }
}