namespace Imprevis.Dataverse.Plugins.UnitTests.Services;

using Imprevis.Dataverse.Plugins.Services;
using System;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using System.Linq;

public class CacheServiceTests
{
    private readonly ICacheService cache;

    public CacheServiceTests()
    {
        cache = new MemoryCacheService();
    }

    [Fact]
    public void Clear_ShouldRemoveItemsFromCache()
    {
        var cacheKey = Guid.NewGuid().ToString();

        cache.GetOrAdd(cacheKey, () => Guid.NewGuid(), TimeSpan.FromMinutes(1));
        Assert.True(cache.Contains(cacheKey));

        cache.Clear();
        Assert.False(cache.Contains(cacheKey));
    }

    [Fact]
    public async Task GetOrAdd_ShouldBlockWhenAddingKey()
    {
        var cacheKey = Guid.NewGuid().ToString();

        var callCount = 0;

        Guid factory()
        {
            Interlocked.Increment(ref callCount);
            Task.Delay(100);
            return Guid.NewGuid();
        }

        var tasks = Enumerable.Range(0, 10).Select(_ => Task.Run(() => cache.GetOrAdd(cacheKey, factory, TimeSpan.FromMinutes(1))));

        await Task.WhenAll(tasks);

        Assert.Equal(1, callCount);
    }

    [Fact]
    public void TryGet_ShouldReturnFalseWhenKeyNotFound()
    {
        var cacheKey = Guid.NewGuid().ToString();
        var exists = cache.TryGet(cacheKey, out Guid value);

        Assert.False(exists);
        Assert.Equal(default, value);
    }

    [Fact]
    public void TryGet_ShouldReturnTrueWhenKeyExists()
    {
        var cacheKey = Guid.NewGuid().ToString();
        var expectedValue = Guid.NewGuid();

        cache.Set(cacheKey, expectedValue, TimeSpan.FromMinutes(1));
        var exists = cache.TryGet(cacheKey, out Guid value);

        Assert.True(exists);
        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public void GetOrDefault_ShouldReturnDefaultWhenKeyNotFound()
    {
        var cacheKey = Guid.NewGuid().ToString();
        var value = cache.Get<Guid>(cacheKey);

        Assert.Equal(default, value);
    }

    [Fact]
    public void GetOrDefault_WithDefaultValue_ShouldReturnDefaultWhenKeyNotFound()
    {
        var cacheKey = Guid.NewGuid().ToString();
        var defaultValue = Guid.NewGuid();
        var value = cache.Get(cacheKey, defaultValue);

        Assert.Equal(defaultValue, value);
    }

    [Fact]
    public void GetOrDefault_ShouldReturnCachedValue()
    {
        var cacheKey = Guid.NewGuid().ToString();
        var expectedValue = Guid.NewGuid();

        cache.Set(cacheKey, expectedValue, TimeSpan.FromMinutes(1));
        var value = cache.Get<Guid>(cacheKey);

        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public void Set_ShouldAllowNullValues()
    {
        var cacheKey = Guid.NewGuid().ToString();

        cache.Set<string>(cacheKey, null, TimeSpan.FromMinutes(1));
        var exists = cache.TryGet<string>(cacheKey, out var value);

        Assert.True(exists);
        Assert.Null(value);
    }
}