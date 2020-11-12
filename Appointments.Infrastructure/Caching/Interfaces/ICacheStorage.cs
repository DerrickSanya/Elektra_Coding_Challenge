namespace Appointments.Infrastructure.Caching.Interfaces
{
    using System;

    /// <summary>
    /// ICacheStore
    /// </summary>
    public interface ICacheStorage
    {
        /// <summary>
        /// AddItem to store
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        /// <param name="key"></param>
        /// <param name="expirationTime"></param>
        void AddItem<TItem>(TItem item, ICacheKey<TItem> key, TimeSpan? expirationTime = null);

        /// <summary>
        /// AddItem to store
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        /// <param name="key"></param>
        /// <param name="absoluteExpiration"></param>
        void AddItem<TItem>(TItem item, ICacheKey<TItem> key, DateTime? absoluteExpiration = null);

        /// <summary>
        /// Update Item
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        /// <param name="key"></param>
        /// <param name="expirationTime"></param>
        void UpdateItem<TItem>(TItem item, ICacheKey<TItem> key, TimeSpan? expirationTime = null);

        /// <summary>
        /// GetItem from store
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TItem GetItem<TItem>(ICacheKey<TItem> key) where TItem : class;

        /// <summary>
        /// Delete item from store
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key"></param>
        void RemoveItem<TItem>(ICacheKey<TItem> key);
    }
}
