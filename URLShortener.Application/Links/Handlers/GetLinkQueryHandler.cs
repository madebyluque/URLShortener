using URLShortener.Application.Links.Queries;
using URLShortener.Application.Shared.Requests;
using URLShortener.Domain.Cache;
using URLShortener.Domain.Entities.Links;
using URLShortener.Domain.Entities.Links.Cache;
using URLShortener.Domain.Entities.Links.Repositories;
using URLShortener.Domain.Shared.Notifications;

namespace URLShortener.Application.Links.Handlers;

public class GetLinkQueryHandler(ILinksRepository linksRepository,
                                 ICacheService cacheService,
                                 NotificationContext notificationContext) : IHandler<GetLinkQuery>
{
    private readonly ILinksRepository _linksRepository = linksRepository;
    private readonly ICacheService _cacheService = cacheService;
    private readonly NotificationContext _notificationContext = notificationContext;

    public async Task<RequestResult> Handle(GetLinkQuery request, CancellationToken cancellationToken)
    {
        if (!request.IsValid)
        {
            _notificationContext.AddNotifications(request.Notifications);
            return null;
        }

        var cacheKey = new LinkCacheKey(request.Id);
        Link link = await _cacheService.GetAsync<Link>(cacheKey);

        if (link != null)
        {
            return new RequestResult(link.Address);
        }

        link = await _linksRepository.GetLinkById(request.Id);

        if (link == null)
        {
            _notificationContext.AddNotification("Link", "It was not possible to find the associated URI"); // TODO: add resource translation
            return null;
        }

        return new RequestResult(link.Address);
    }
}
