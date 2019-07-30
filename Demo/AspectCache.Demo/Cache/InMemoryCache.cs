
using AspectCache.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AspectCache.Demo.Cache
{
    public class InMemoryCache : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void Set<T>(string key, object value)
        {
            _memoryCache.Set<T>(key, (T)value);
        }
    }
}
