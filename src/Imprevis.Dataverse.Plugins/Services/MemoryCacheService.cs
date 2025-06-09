namespace Imprevis.Dataverse.Plugins.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.Caching;
    using System.Threading;

    internal class MemoryCacheService : ICacheService
    {
        private static readonly MemoryCache cache = MemoryCache.Default;
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> locks = new ConcurrentDictionary<string, SemaphoreSlim>();

        private static readonly object placeholder = new object();

        public void Clear()
        {
            foreach (var item in cache)
            {
                Remove(item.Key);
            }
        }

        public bool Contains(string key)
        {
            return cache.Contains(key);
        }

        public bool Get<T>(string key, out T value)
        {
            var tempValue = cache.Get(key);
            if (tempValue == null)
            {
                value = default;
                return false;
            }

            if (tempValue == placeholder)
            {
                value = default;
                return true;
            }

            value = (T)tempValue;
            return true;
        }

        public T GetOrAdd<T>(string key, T value, TimeSpan duration)
        {
            return GetOrAdd(key, () => value, duration);
        }

        public T GetOrAdd<T>(string key, Func<T> factory, TimeSpan duration)
        {
            var exists = Get<T>(key, out var value);

            // Item exists in cache, return it.
            if (exists)
            {
                return value;
            }

            // Item doesn't exist, let's take out a lock and check again.
            var _lock = locks.GetOrAdd(key, new SemaphoreSlim(1, 1));
            _lock.Wait();
            
            try
            {
                // Now that we have an exclusive lock, try to get the item again.
                exists = Get<T>(key, out value);

                // Item still doesn't exist in cache, so run the factory method to get it. The factory method
                // will only run once concurrently because of the lock on the cache key.
                if (!exists)
                {
                    value = factory();
                    Set(key, value, duration);
                }
            }
            finally
            {
                // Release lock so other threads can try to get the value.
                _lock.Release();
            }

            return value;
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public void Set<T>(string key, T value, TimeSpan duration)
        {
            // MemoryCache doesn't support setting null values. Use a random object as a placeholder.
            // This allows us to explicitly cache a null value to avoid cache misses.
            if (value == null)
            {
                cache.Set(key, placeholder, DateTime.UtcNow.Add(duration));
            }
            else
            {
                cache.Set(key, value, DateTime.UtcNow.Add(duration));
            }
        }
    }
}
