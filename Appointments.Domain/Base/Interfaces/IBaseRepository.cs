using System.Collections.Generic;
using System.Threading.Tasks;

namespace Appointments.Domain.Base.Interfaces
{
    /// <summary>
    /// IBaseRepository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region Async

        /// <summary>
        /// The get all async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// The get many async.
        /// </summary>
        /// <param name="filters">
        /// The filters.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<IEnumerable<TEntity>> GetManyAsync(object filters);

        /// <summary>
        /// The get by scalar value async.
        /// </summary>
        /// <param name="filters">
        /// The filters.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TEntity> GetByScalarValueAsync(object filters);

        /// <summary>
        /// ExistsAsync
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(object filters);

        /// <summary>
        /// The add async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// The update async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> DeleteAsync(object key);

        #endregion
    }
}