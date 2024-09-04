namespace URLShortener.Domain.Entities.Links.Events;

public class LinkCreatedDomainEvent(Link link) : BaseLinkDomainEvent(link)
{
}
