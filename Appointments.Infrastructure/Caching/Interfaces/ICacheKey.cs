namespace Appointments.Infrastructure.Caching.Interfaces
{
    /// <summary>
    /// ICacheKey<TItem>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public interface ICacheKey<TItem>
    {
        /// <summary>
        /// CacheKey
        /// </summary>
        string CacheKey { get; }
    }
}