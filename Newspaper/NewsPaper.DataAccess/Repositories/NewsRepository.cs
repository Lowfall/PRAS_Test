using NewsPaper.DataAccess.Data;
using Newspaper.Domain.Entities;
using Newspaper.Domain.Interfaces.Repositories;

namespace NewsPaper.DataAccess.Repositories;

public class NewsRepository(AppDbContext dbContext) : BaseRepository<News>(dbContext), INewsRepository { }