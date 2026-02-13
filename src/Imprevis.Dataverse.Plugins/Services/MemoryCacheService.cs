namespace Imprevis.Dataverse.Plugins.Services;

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;

internal class MemoryCacheService : ICacheService
{
    private static readonly MemoryCache cache = MemoryCache.Default;
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> locks = new();

    /// <summary>
    /// Cache item wrapper to distinguish between cache misses and null values.
    /// </summary>
    private sealed class CacheItem<T>(T? value)
    {
        public T? Value { get; } = value;
    }

    public void Clear()
    {
        // Get list of keys first to avoid collection modified exception.
        var keys = cache.Select(kvp => kvp.Key).ToList();

        foreach (var key in keys)
        {
            Remove(key);
        }
    }

    public bool Contains(string key)
    {
        return cache.Contains(key);
    }

    public T? Get<T>(string key)
    {
        TryGet<T>(key, out var value);
        return value;
    }

    public T Get<T>(string key, T defaultValue)
    {
        return TryGet<T>(key, out var value) && value is not null ? value : defaultValue;
    }

    public bool TryGet<T>(string key, out T? value)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or whitespace.", nameof(key));
        }

        var cachedValue = cache.Get(key);
        if (cachedValue is not CacheItem<T> item)
        {
            value = default;
            return false;
        }

        value = item.Value;
        return true;
    }

    public T GetOrAdd<T>(string key, T value, TimeSpan duration)
    {
        return GetOrAdd(key, () => value, duration);
    }

    public T GetOrAdd<T>(string key, Func<T> factory, TimeSpan duration)
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        var exists = TryGet<T>(key, out var value);

        // Item exists in cache, return it.
        if (exists && value is not null)
        {
            return value;
        }

        // Item doesn't exist, let's take out a lock and check again.
        var _lock = locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        _lock.Wait();

        try
        {
            // Now that we have an exclusive lock, try to get the item again.
            exists = TryGet<T>(key, out value);

            // Item still doesn't exist in cache, so run the factory method to get it. The factory method
            // will only run once concurrently because of the lock on the cache key.
            if (!exists || value is null)
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
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or whitespace.", nameof(key));
        }

        cache.Remove(key);
    }

    public void Set<T>(string key, T? value, TimeSpan duration)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or whitespace.", nameof(key));
        }

        if (duration <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(duration), "Duration must be greater than zero.");
        }

        // Wrap the value in a CacheItem to distinguish between null values and missing entries.
        var entry = new CacheItem<T>(value);
        var policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.UtcNow.Add(duration),
            RemovedCallback = OnCacheEntryRemoved
        };

        cache.Set(key, entry, policy);
    }

    private void OnCacheEntryRemoved(CacheEntryRemovedArguments arguments)
    {
        // Clean up the lock when the cache entry is removed (expired or evicted)
        if (locks.TryRemove(arguments.CacheItem.Key, out var removedLock))
        {
            removedLock.Dispose();
        }
    }
}
