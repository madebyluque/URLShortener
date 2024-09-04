using URLShortener.Domain.Cache;

namespace URLShortener.Domain.Entities.Links.Cache;

public class LinkCacheKey(string id) : CacheKey(nameof(Link), id)
{
}
