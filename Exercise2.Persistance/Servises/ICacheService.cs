using Exercise2.Domain.Common.Entities;
using Exercise2.Domain.Common.Settings;
using Microsoft.Extensions.Caching.Distributed;

namespace Exercise2.Persistance.Servises;

public interface ICacheService
{
    ValueTask<Laptop?> GetAsync(string key);

    List<Laptop> GetAll();

    DistributedCacheEntryOptions GetCacheEntryOptions(CacheEntryOptions? entryOptions = default);

    ValueTask SetAsync<Laptop>(string key, Laptop value, CacheEntryOptions? entryOptions = default);

    ValueTask DeleteAsync(string key);
}