using FluentValidation;
using URLShortener.Application.Links.Commands;

namespace URLShortener.API.Extensions;

public static class FluentValidationsExtension
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ShortenLinkCommandValidator>();
        return services;
    }
}
