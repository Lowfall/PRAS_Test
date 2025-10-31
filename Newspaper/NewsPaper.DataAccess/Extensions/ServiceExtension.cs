using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsPaper.DataAccess.Data;
using NewsPaper.DataAccess.Options;

namespace NewsPaper.DataAccess.Extensions;

public static class ServiceExtension
{
    public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));
        
        services.AddDbContext<AppDbContext>();
    }
}