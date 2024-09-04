namespace URLShortener.Domain.Cache;

public class CacheKey(string prefix, string entityId)
{
    public string Key { get; set; } = CreateKey(prefix, entityId);

    private static string CreateKey(string prefix, string entityId) => $"{prefix}-{entityId}";
}
