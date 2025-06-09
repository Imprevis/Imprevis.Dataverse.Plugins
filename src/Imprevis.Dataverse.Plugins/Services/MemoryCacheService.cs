namespace Imprevis.Dataverse.Plugins.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Runtime.Caching;
    using System.Threading;

    internal class MemoryCacheService : ICacheService
    {
        private static readonly MemoryCache cache = MemoryCache.Default;
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> locks = new ConcurrentDictionary<string, SemaphoreSlim>();

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

        public T Get<T>(string key)
        {
            var value = cache.Get(key);
            if (value == null)
            {
                return default;
            }
            return (T)value;
        }

        public T GetOrAdd<T>(string key, T value, TimeSpan duration)
        {
            return GetOrAdd(key, () => value, duration);
        }

        public T GetOrAdd<T>(string key, Func<T> factory, TimeSpan duration)
        {
            var value = Get<T>(key);
            if (!IsDefault(value))
            {
                return value;
            }

            var _lock = locks.GetOrAdd(key, new SemaphoreSlim(1, 1));
            _lock.Wait();

            value = Get<T>(key);

            if (IsDefault(value))
            {
                value = factory();
                Set(key, value, duration);
            }

            _lock.Release();

            return value;
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public void Set<T>(string key, T value, TimeSpan duration)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            cache.Set(key, value, DateTime.UtcNow.Add(duration));
        }

        private bool IsDefault<T>(T value)
        {
            return EqualityComparer<T>.Default.Equals(value, default);
        }
    }
}
