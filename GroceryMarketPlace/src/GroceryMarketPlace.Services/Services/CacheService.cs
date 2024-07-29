namespace GroceryMarketPlace.Services.Services
{
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Domain.Interfaces.Services;
    using Domain.Models;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using StackExchange.Redis;

    public class CacheService(
        ILogger<CacheService> logger,
        IDistributedCache cache,
        IConnectionMultiplexer connectionMultiplexer,
        IOptions<CacheSettings> cacheSettings) : ICacheService
    {
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        public async Task<T?> GetDataAsync<T>(string key)
        {
            logger.LogInformation("Get cache by key {@key}", key);

            var jsonData = await cache.GetStringAsync(key);

            if (jsonData == null)
            {
                return default;
            }

            logger.LogInformation("Get cache by key {@key} successfully", key);

            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public async Task SetDataAsync<T>(string key, T value)
        {
            logger.LogInformation("Set cache with key {@key}", key);

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(cacheSettings.Value.AbsoluteExpirationInMinutes))
                .SetSlidingExpiration(TimeSpan.FromMinutes(cacheSettings.Value.SlidingExpirationInMinutes));

            var jsonData = JsonSerializer.Serialize(value, this._serializerOptions);

            await cache.SetStringAsync(key, jsonData, options);

            logger.LogInformation("Set cache with key {@key} successfully", key);
        }

        public async Task RemoveCachedDataByKeyAsync(string key)
        {
            logger.LogInformation("Remove cache by key {@key}", key);

            await cache.RemoveAsync(key);

            logger.LogInformation("Remove cache by key {@key} successfully", key);
        }

        public async Task RemoveCachedDataByKeyPrefixAsync(string prefix)
        {
            logger.LogInformation("Remove cache by prefix {@prefix}", prefix);

            foreach (var endpoint in connectionMultiplexer.GetEndPoints())
            {
                var server = connectionMultiplexer.GetServer(endpoint);

                // Get all keys that match pattern
                var keys = server.Keys(pattern: $"*{prefix}*");

                foreach (var key in keys)
                {
                    // Remove the cache by key
                    await this.RemoveCachedDataByKeyAsync(key.ToString());
                }
            }

            logger.LogInformation("Remove cache by prefix {@prefix} successfully", prefix);
        }
    }
}
