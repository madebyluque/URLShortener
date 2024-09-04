using MediatR;
using URLShortener.Domain.Cache;
using URLShortener.Domain.Entities.Links.Cache;
using URLShortener.Domain.Entities.Links.Events;

namespace URLShortener.Application.Links.Handlers.Events;

public class LinkCreatedDomainEventHandler(ICacheService cacheService) : INotificationHandler<LinkCreatedDomainEvent>
{
    private readonly ICacheService _cacheService = cacheService;

    public async Task Handle(LinkCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (!_cacheService.IsConnected())
        {
            return;
        }

        await _cacheService.SetAsync(new LinkCacheEntry(notification.Link));
    }

}
