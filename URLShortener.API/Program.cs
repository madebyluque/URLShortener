using URLShortener.API.Extensions;
using URLShortener.Application.Links.Handlers;
using URLShortener.Domain.Shared.Notifications;
using URLShortener.Infrastructure.Module;

namespace URLShortener.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;

        services.ConfigureCors();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddConfiguredSwagger();

        services.AddScoped<NotificationContext>();
        InfrastructureDataModule.Register(builder.Services, builder.Configuration);
        FluentValidationsExtension.AddFluentValidation(builder.Services);
        VersioningExtension.AddVersioning(builder.Services);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ShortenLinkCommandHandler).Assembly));

        var app = builder.Build();

        app.UseConfiguredCors();
        app.UseConfiguredSwagger();
        // app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
