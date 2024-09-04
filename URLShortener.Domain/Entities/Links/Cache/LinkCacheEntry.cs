using URLShortener.Domain.Cache;

namespace URLShortener.Domain.Entities.Links.Cache;

public class LinkCacheEntry(Link entity) : CacheEntry<Link>(entity)
{
}
