using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsPaper.DataAccess.Data;
using NewsPaper.DataAccess.Options;
using NewsPaper.DataAccess.Repositories;
using Newspaper.Domain.Interfaces.Repositories;

namespace NewsPaper.DataAccess.Extensions;

public static class ServiceExtension
{
    public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));
        
        services.AddDbContext<AppDbContext>();
        
        services.AddScoped<INewsRepository, NewsRepository>();
    }
}