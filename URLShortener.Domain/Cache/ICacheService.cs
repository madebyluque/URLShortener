using URLShortener.Domain.Shared.Entities;

namespace URLShortener.Domain.Cache;

public interface ICacheService
{
    Task<T> GetAsync<T>(CacheKey cacheEntry) where T : Entity;
    Task SetAsync<T>(CacheEntry<T> cacheEntry) where T : Entity;
    Task RemoveAsync(CacheKey cacheEntry);
    Task ClearAsync();
    bool IsConnected();
}
