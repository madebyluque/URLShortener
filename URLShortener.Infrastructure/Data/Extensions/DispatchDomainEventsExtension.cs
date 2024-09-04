using MediatR;
using URLShortener.Domain.Entities.Links;
using URLShortener.Infrastructure.Data.Contexts;

namespace URLShortener.Infrastructure.Data.Extensions;

public static class DispatchDomainEventsExtension
{
    public static async Task DispatchDomainEvents(this IMediator mediator, UriShortenerContext context, CancellationToken cancellationToken)
    {
        var entities = context.ChangeTracker.Entries<Link>()
                                                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Count != 0);

        var domainEvents = entities.SelectMany(x => x.Entity.DomainEvents)
                                   .ToList();

        foreach (var item in entities)
        {
            item.Entity.ClearDomainEvents();
        }

        foreach (var item in domainEvents)
        {
            await mediator.Publish(item, cancellationToken);
        }
    }
}
