namespace URLShortener.API.Extensions;

public static class CorsExtensions
{
    private const string CorsPolicy = "_corsPolicy";

    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: CorsPolicy, policy =>
            {
                policy.WithOrigins("http://localhost:3000")
                      .WithMethods("GET", "POST")
                      .AllowAnyHeader();
            });
        });

        return services;
    }

    public static IApplicationBuilder UseConfiguredCors(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicy);

        return app;
    }
}
