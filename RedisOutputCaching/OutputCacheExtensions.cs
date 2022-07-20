using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;

namespace RedisOutputCaching;

public static class OutputCacheExtensions
{
    /// <summary>
    /// Add output caching services using Redis.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> for adding services.</param>
    /// <returns></returns>
    public static IServiceCollection AddRedisOutputCache(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

        services.TryAddSingleton<IOutputCacheStore>(sp =>
        {
            var distributedCache = sp.GetRequiredService<IDistributedCache>();
            var redisCacheOptions = sp.GetRequiredService<IOptions<RedisCacheOptions>>();
            return new RedisOutputCacheStore(distributedCache, redisCacheOptions);
        });
        return services;
    }
}