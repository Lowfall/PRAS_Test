using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newspaper.Domain.Interfaces.Services;
using Newspaper.Infrastructure.Options;
using Newspaper.Infrastructure.Services;

namespace Newspaper.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ExternalServicesOptions>(configuration.GetSection(nameof(ExternalServicesOptions)));
        
        services.AddScoped<IImageService, ImageService>();
        services.AddHttpClient<ITranslatorService, TranslatorService>(
            client => client.BaseAddress = new Uri(configuration.GetSection("ExternalServicesOptions").GetSection("TranslatorOptions")["ApiEndpoint"]));
    }
}