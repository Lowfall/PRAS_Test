using Microsoft.EntityFrameworkCore;
using NewsPaper.DataAccess.Data;
using Newspaper.Domain.Entities;
using Newspaper.Domain.Interfaces.Repositories;

namespace NewsPaper.DataAccess.Repositories;

public class NewsRepository(AppDbContext dbContext) : BaseRepository<News>(dbContext), INewsRepository
{
    //better to implement Specification pattern, but I did it like that
    
    public async Task<News> GetNewsWithTranslationsAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbSet.Include(n => n.Translations).FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
    }
    
    public async Task<List<News>> GetNLatestNewsAsync(int amount, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(n => n.Translations)
            .OrderByDescending(n => n.CreatedAt)
            .Take(amount)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<News>> GetPaginatedNewsAsync(int page, int limit, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(n => n.Translations)
            .OrderByDescending(n => n.CreatedAt)
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }
}