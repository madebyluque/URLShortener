using URLShortener.Domain.Entities.Links.Events;
using URLShortener.Domain.Shared.Entities;

namespace URLShortener.Domain.Entities.Links;

public class Link : Entity
{
    public Link()
    {

    }

    public Link(Uri address)
    {
        Address = address;
        Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
        CreatedAt = DateTime.UtcNow;

        AddDomainEvent(new LinkCreatedDomainEvent(this));
    }

    public Uri Address { get; set; }
}
