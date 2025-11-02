using Microsoft.EntityFrameworkCore;
using NewsPaper.DataAccess.Data;
using Newspaper.Domain.Interfaces.Repositories;

namespace NewsPaper.DataAccess.Repositories;

public class BaseRepository <TEntity> : IBaseRepository<TEntity> where  TEntity : class
{
    protected readonly DbSet<TEntity> _dbSet;
    protected readonly AppDbContext _dbContext;

    protected BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<int> GetAmountAsync()
    {
        return await _dbSet.AsNoTracking().CountAsync();
    }
    
    public async Task<TEntity> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }
    
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}