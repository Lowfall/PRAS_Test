using System.Reflection;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

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
    }
}