using Asp.Versioning;

namespace URLShortener.API.Extensions;

public static class VersioningExtension
{
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(x =>
        {
            x.DefaultApiVersion = new ApiVersion(1, 0);
            x.ReportApiVersions = true;
            x.AssumeDefaultVersionWhenUnspecified = true;
        });

        return services;
    }
}
