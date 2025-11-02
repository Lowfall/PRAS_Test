using Newspaper.Domain.Enums;

namespace Newspaper.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    Task<int> GetAmountAsync();
    Task<TEntity> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
}