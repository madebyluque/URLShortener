using URLShortener.Domain.Shared.Events;

namespace URLShortener.Domain.Shared.Entities;
public class Entity
{
    public Entity()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }

    private List<IDomainEvent> _domainEvents;
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}