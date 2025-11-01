using System.Reflection;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Newspaper.Application.DTOs.News.Request;
using Newspaper.Domain.Entities;

namespace Newspaper.Application.Extensions;

public static class ServiceExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
       services.AddMediatR(cfg =>
       { 
           cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
       });

       services.AddMapster();
       
       TypeAdapterConfig<NewsDTO, News>
           .NewConfig()
           .IgnoreNullValues(true);
       
       TypeAdapterConfig<TranslationDTO, NewsTranslations>
           .NewConfig()
           .IgnoreNullValues(true);
    }
}