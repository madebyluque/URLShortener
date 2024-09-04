using URLShortener.Domain.Shared.Entities;

namespace URLShortener.Domain.Cache;

public abstract class CacheEntry<T>(T entity) where T : Entity
{
    public T Entity { get; set; } = entity;
    public CacheKey Key { get; set; } = new CacheKey(typeof(T).Name, entity.Id);
}