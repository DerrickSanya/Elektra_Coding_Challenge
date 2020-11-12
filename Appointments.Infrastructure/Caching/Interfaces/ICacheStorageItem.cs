namespace Appointments.Infrastructure.Caching.Interfaces
{
    /// <summary>
    /// ICacheStorageItem
    /// </summary>
    public interface ICacheStorageItem
    {
        /// <summary>
        /// CacheKey
        /// </summary>
        string CacheKey { get; }
    }
}