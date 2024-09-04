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

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddConfiguredSwagger();
        builder.Services.AddScoped<NotificationContext>();
        InfrastructureDataModule.Register(builder.Services, builder.Configuration);
        FluentValidationsExtension.AddFluentValidation(builder.Services);
        VersioningExtension.AddVersioning(builder.Services);
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ShortenLinkCommandHandler).Assembly));

        var app = builder.Build();

        app.UseConfiguredSwagger();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
