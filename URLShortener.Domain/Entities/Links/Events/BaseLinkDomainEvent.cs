using MediatR;
using URLShortener.Domain.Shared.Events;

namespace URLShortener.Domain.Entities.Links.Events;

public abstract class BaseLinkDomainEvent(Link link) : INotification, IDomainEvent
{
    public Link Link { get; set; } = link;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
