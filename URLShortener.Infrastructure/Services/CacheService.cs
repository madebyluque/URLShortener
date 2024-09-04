using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;
using URLShortener.Domain.Cache;
using URLShortener.Domain.Shared.Entities;

namespace Chat.CrossCutting.Cache;

public class CacheService(IConfiguration configuration) : ICacheService, IDisposable
{
    private readonly ConnectionMultiplexer _redisConnection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
    private readonly int _expirationInMinutes = int.Parse(configuration["CacheConfigurations:ExpiryInMinutes"]);

    public async Task ClearAsync()
    {
        if (IsConnected())
        {
            var db = _redisConnection.GetDatabase();
            await db.ExecuteAsync("FLUSHDB");
        }
    }

    public async Task<T> GetAsync<T>(CacheKey cacheKey) where T : Entity
    {
        if (IsConnected())
        {
            var db = _redisConnection.GetDatabase();
            var serializedEntity = await db.StringGetAsync(cacheKey.Key);

            if (serializedEntity == RedisValue.Null)
            {
                return null;
            }

            return JsonSerializer.Deserialize<T>(serializedEntity);
        }

        return null;
    }

    public async Task RemoveAsync(CacheKey cacheKey)
    {
        if (IsConnected())
        {
            var db = _redisConnection.GetDatabase();
            await db.KeyDeleteAsync(cacheKey.Key);
        }
    }

    public async Task SetAsync<T>(CacheEntry<T> cacheEntry) where T : Entity
    {
        if (IsConnected())
        {
            var serializedEntity = JsonSerializer.Serialize(cacheEntry.Entity);
            var db = _redisConnection.GetDatabase();

            await db.StringSetAsync(cacheEntry.Key.Key, serializedEntity, TimeSpan.FromMinutes(_expirationInMinutes));
        }
    }

    public bool IsConnected() => _redisConnection.IsConnected;

    public void Dispose()
    {
        _redisConnection.Dispose();
    }
}
