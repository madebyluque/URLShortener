using Chat.CrossCutting.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using URLShortener.Domain.Cache;
using URLShortener.Domain.Entities.Links.Repositories;
using URLShortener.Infrastructure.Data.Contexts;
using URLShortener.Infrastructure.Data.Links.Repositories;

namespace URLShortener.Infrastructure.Module;

public static class InfrastructureDataModule
{
    public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        Migrate(configuration);
        AddCacheServices(services, configuration);
        AddRepositories(services);

        return services;
    }

    private static void Migrate(IConfiguration configuration)
    {
        using (var context = new UriShortenerContext(configuration))
        {
            context.Database.Migrate();
        }
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UriShortenerContext>();
    }

    private static void AddCacheServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICacheService, CacheService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<ILinksRepository, LinksRepository>();
    }
}
