namespace Imprevis.Dataverse.Plugins.UnitTests.Services
{
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
        public void GetOrAdd_ShouldAddNullValues()
        {
            var cacheKey = Guid.NewGuid().ToString();

            cache.GetOrAdd(cacheKey, (string)null, TimeSpan.FromMinutes(1));
            var exists = cache.Get(cacheKey, out string value);

            Assert.True(exists);
            Assert.Null(value);
        }
    }
}