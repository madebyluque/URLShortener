using Microsoft.OpenApi.Models;

namespace URLShortener.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "URL Shortener API",
                Description = "An URL shortener api.",
                Contact = new OpenApiContact
                {
                    Name = "Lucas Silva",
                    Email = "lukesilva.dev@gmail.com"
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseConfiguredSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
