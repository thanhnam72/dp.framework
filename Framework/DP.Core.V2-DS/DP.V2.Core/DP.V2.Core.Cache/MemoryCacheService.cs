using Microsoft.Extensions.Caching.Memory;
using System;

namespace DP.V2.Core.Cache
{
    public class MemoryCacheService : ICachingService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T GetObject<T>(string cacheKey, int expireInMinute, Func<T> objectFunction)
        {
            if (_cache.TryGetValue(cacheKey, out T cachedObject))
            {
                return cachedObject;
            }

            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expireInMinute),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };

            cachedObject = objectFunction();

            _cache.Set(cacheKey, cachedObject, options);

            return cachedObject;
        }

        public T GetObject<T>(string cacheKey, Func<T> objectFunction)
        {
            return GetObject(cacheKey, 60, objectFunction);
        }

        public void Remove(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }
    }
}
