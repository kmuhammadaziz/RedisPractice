using System.Net;
using System.Text;
using Exercise2.Domain.Common.Entities;
using Exercise2.Domain.Common.Settings;
using Force.DeepCloner;
using Microsoft.Extensions.Caching.Distributed; 
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Exercise2.Persistance.Servises;

public class CacheService(IDistributedCache distributedCache) : ICacheService
{
    private readonly DistributedCacheEntryOptions _entryOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1200),
        SlidingExpiration = TimeSpan.FromSeconds(300)
    };

    public async ValueTask<Laptop?> GetAsync(string key)
    {
        var value = await distributedCache.GetAsync(key);

        return value is not null ? JsonConvert.DeserializeObject<Laptop>(Encoding.UTF8.GetString(value)) : default;
    }

    public async ValueTask SetAsync<Laptop>(string key, Laptop value, CacheEntryOptions? entryOptions = default)
    {
        await distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value), GetCacheEntryOptions(entryOptions));
    }

    public ValueTask DeleteAsync(string key)
    {
        distributedCache.Remove(key);
        return ValueTask.CompletedTask;
    }

    public DistributedCacheEntryOptions GetCacheEntryOptions(CacheEntryOptions? entryOptions = default)
    {
        if (entryOptions == default || (!entryOptions.AbsoluteExpirationRelativeToNow.HasValue && !entryOptions.SlidingExpiration.HasValue))
            return _entryOptions;

        var currentEntryOptions = _entryOptions.DeepClone();

        currentEntryOptions.AbsoluteExpirationRelativeToNow = entryOptions.AbsoluteExpirationRelativeToNow;
        currentEntryOptions.SlidingExpiration = entryOptions.SlidingExpiration;

        return currentEntryOptions;
    }

    public List<Laptop> GetAll()
    {
        var result = new List<Laptop>();

        ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("localhost:6379");
        EndPoint endPoint = connection.GetEndPoints().First(); 
        RedisKey[] keys = connection.GetServer(endPoint).Keys(pattern: "*").ToArray();

        for(int i = 0; i < keys.Length; i++)
        { result.Add(GetAsync(keys.GetValue(i)!.ToString()!.Replace("Caching.Example", "")).Result!); }

        return result;
    }
}