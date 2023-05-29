using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Chat.Application.Services;
using Chat.Domain.Attributes;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;


namespace Chat.Infra.Data.Cache
{
    [Bean]
    public class Cache : ICache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public Cache(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<T> Get<T>(string key)
        {
            var value = await _distributedCache.GetStringAsync(key);

            return value != null ? JsonConvert.DeserializeObject<T>(value) : default;
        }

        public Task Set<T>(string key, T value)
        {
            return _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value));
        }

        public Task Set<T>(string key, T value, DateTimeOffset absoluteExpiration)
        {
            return _distributedCache.SetStringAsync(key,
                JsonConvert.SerializeObject(value),
                new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = absoluteExpiration
                });
        }

        public Task Remove(string key)
        {
            return _distributedCache.RemoveAsync(key);
        }  
        // public Task<List<string>> GetAllKeys()
        // {
        //     return _connectionMultiplexer
        //         .GetServer(_connectionMultiplexer
        //             .GetEndPoints()
        //             .First())
        //         .KeysAsync(pattern: "*")
        //         .Select(key => key.ToString())
        //         .ToListAsync()
        //         .AsTask();
        // }
    }
}