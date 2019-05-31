using System;
using System.Threading;
using Xunit;

namespace Caching.UnitTests
{
    public class LRUCacheTest
    {
        [Fact]
        public void AddNewItem()
        {
            var cache = new LRUCache(2);

            cache.Put(1, 999);

            var value = cache.Get(1);

            Assert.Equal(999, value);
        }

        [Fact]
        public void UpdateValueIfItemIsExisting()
        {
            var cache = new LRUCache(2);

            cache.Put(1, 1);
            cache.Put(1, 110);

            var value = cache.Get(1);

            Assert.Equal(110, value);
        }

        [Fact]
        public void RemoveLeastRecentlyUsedWhenCacheReachCapacity()
        {
            var cache = new LRUCache(2);

            cache.Put(1, 1);
            cache.Put(2, 2);
            cache.Put(3, 3);

            var value = cache.Get(1);

            Assert.Equal(-1, value);
        }

        [Fact]
        public void ThrowInvalidCacheSizeExceptionWhenCapacityLessThanZero()
        {
            Action createLRUCacheWithCapacityLessThanZero = () => new LRUCache(-1);

            Assert.Throws<InvalidCacheSizeException>(createLRUCacheWithCapacityLessThanZero);
        }

        [Fact]
        public void EnsureCacheIsThreadSafe()
        {
            int iterations = 100;
            var cache = new LRUCache(2);
            cache.Put(1, 0);

            Action createValueByHundred = () => 
            {
                for (int i = 0; i < iterations; i++)
                {
                    var value = cache.Get(1);
                    cache.Put(1, value + 1);
                }
            };

            Thread thread1 = new Thread(new ThreadStart(createValueByHundred));
            Thread thread2 = new Thread(new ThreadStart(createValueByHundred));

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            var result = cache.Get(1);

            Assert.Equal(200, result);
        }
    }
}
