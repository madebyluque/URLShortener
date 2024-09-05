using URLShortener.Application.Links.Commands;
using URLShortener.Application.Shared.Requests;
using URLShortener.Domain.Entities.Links;
using URLShortener.Domain.Entities.Links.Repositories;
using URLShortener.Domain.Shared.Notifications;

namespace URLShortener.Application.Links.Handlers;

public class ShortenLinkCommandHandler(ILinksRepository linksRepository,
                                       NotificationContext notificationContext) : IHandler<ShortenLinkCommand>
{
    private readonly ILinksRepository _linksRepository = linksRepository;
    private readonly NotificationContext _notificationContext = notificationContext;

    public async Task<RequestResult> Handle(ShortenLinkCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid)
        {
            _notificationContext.AddNotifications(request.Notifications);
            return null;
        }

        // TODO: search link in cache
        var existentLink = await _linksRepository.GetLinkByAddress(request.Address);

        if (existentLink != null)
        {
            return new RequestResult(existentLink.Id);
        }

        var link = new Link(request.Address);
        await _linksRepository.CreateLink(link, cancellationToken);

        return new RequestResult(link.Id);
    }
}
