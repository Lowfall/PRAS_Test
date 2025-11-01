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
}