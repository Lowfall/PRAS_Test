using Newspaper.Domain.Entities;

namespace Newspaper.Domain.Interfaces.Repositories;

public interface INewsRepository : IBaseRepository<News>
{
    Task<News> GetNewsWithTranslationsAsync(int id, CancellationToken cancellationToken);
    Task<List<News>> GetNLatestNewsAsync(int amount, CancellationToken cancellationToken);
    Task<List<News>> GetPaginatedNewsAsync(int page, int limit, CancellationToken cancellationToken);
}