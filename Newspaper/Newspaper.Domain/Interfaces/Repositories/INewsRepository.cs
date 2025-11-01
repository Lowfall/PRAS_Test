using Newspaper.Domain.Entities;

namespace Newspaper.Domain.Interfaces.Repositories;

public interface INewsRepository : IBaseRepository<News>
{
    Task<News> GetNewsWithTranslationsAsync(int id, CancellationToken cancellationToken);
}