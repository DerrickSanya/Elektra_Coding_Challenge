namespace Appointments.Infrastructure.Caching
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;
    using Appointments.Infrastructure.Caching.Interfaces;

    /// <summary>
    /// CacheStorage
    /// </summary>
    public class CacheStorage : ICacheStorage
    {
        /// <summary>
        /// IMemoryCache _memoryCache
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// IDictionary<string, TimeSpan> _expirationConfig
        /// </summary>
        private readonly IDictionary<string, TimeSpan> _expirationConfig;

        /// <summary>
        /// Cache Storage .actor
        /// </summary>
        /// <param name="_memoryCache"></param>
        /// <param name="_expirationConfiguration"></param>
        public CacheStorage(IMemoryCache memoryCache, IDictionary<string, TimeSpan> expirationConfiguration)
        {
            _memoryCache = memoryCache;
            _expirationConfig = expirationConfiguration;
        }

        /// <summary>
        /// Adds an Item to the memory cache
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        /// <param name="key"></param>
        /// <param name="expirationTime"></param>
        public void AddItem<TItem>(TItem item, ICacheKey<TItem> key, TimeSpan? expirationTime = null)
        {
            var cachedObjectName = item.GetType().Name;
            TimeSpan timespan;
            if (expirationTime.HasValue)
                timespan = expirationTime.Value;
            else
                timespan = _expirationConfig[cachedObjectName];

            _memoryCache.Set(key.CacheKey, item, timespan);
        }

        /// <summary>
        /// AddItem to the cache storage
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        /// <param name="key"></param>
        /// <param name="absoluteExpiration"></param>
        public void AddItem<TItem>(TItem item, ICacheKey<TItem> key, DateTime? absoluteExpiration = null)
        {
            DateTimeOffset offset;
            if (absoluteExpiration.HasValue)
                offset = absoluteExpiration.Value;
            else
                offset = DateTimeOffset.MaxValue;

            _memoryCache.Set(key.CacheKey, item, offset);
        }

        /// <summary>
        /// Update Item in the Cache store
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        /// <param name="key"></param>
        /// <param name="expirationTime"></param>
        public void UpdateItem<TItem>(TItem item, ICacheKey<TItem> key, TimeSpan? expirationTime = null)
        {
            var cachedObjectName = item.GetType().Name;
            TimeSpan timespan;
            if (expirationTime.HasValue)
                timespan = expirationTime.Value;
            else
                timespan = _expirationConfig[cachedObjectName];

            // remove old item
            _memoryCache.Remove(key.CacheKey);

            // add new item
            _memoryCache.Set(key.CacheKey, item, timespan);
        }

        /// <summary>
        /// Retrieves an item from the cached storage
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TItem GetItem<TItem>(ICacheKey<TItem> key) where TItem : class
        {
            if (_memoryCache.TryGetValue(key.CacheKey, out TItem value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// RemoveItem
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key"></param>
        public void RemoveItem<TItem>(ICacheKey<TItem> key)
        {
            _memoryCache.Remove(key.CacheKey);
        }
    }
}
