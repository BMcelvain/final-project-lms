using Microsoft.Extensions.Caching.Memory;
using System;
using Lms.Cache;

namespace Lms.Cache
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public void Add<T>(string key, T value, TimeSpan duration)
        {
            _memoryCache.Set(key, value, duration);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }

    public interface ICacheProvider
    {
        T Get<T>(string key);
        void Add<T>(string key, T value, TimeSpan duration);
        void Remove(string key);
    }


}
