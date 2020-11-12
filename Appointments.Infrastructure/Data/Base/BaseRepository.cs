using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Appointments.Utilities.SqlGenerator;
using Appointments.Infrastructure.Database;
using Appointments.Utilities.DependencyInjection;
using Appointments.Domain.Base.Interfaces;

namespace Appointments.Infrastructure.Data.Base
{
    /// <summary>
    /// Base Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// The conn.
        /// </summary>
        private readonly IDbConnection _conn;

        /// <summary>
        /// Initialises a new instance of the <see cref="BaseRepository{T}"/> class. 
        /// </summary>
        protected BaseRepository()
        {
            _conn = DependencyResolver.Current.GetInstance<ISqlConnectionManager>().DbConnection();
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        protected IDbConnection Connection => _conn ?? DependencyResolver.Current.GetInstance<ISqlConnectionManager>().DbConnection();

        /// <summary>
        /// Gets the SQL generator.
        /// </summary>
        // protected ISqlBuilder<T> SqlGenerator => new SqlBuilder<T>();
        protected ISqlBuilder<TEntity> SqlGenerator => DependencyResolver.Current.GetInstance<ISqlBuilder<TEntity>>();

        #region Async
        /// <inheritdoc />
        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var sql = SqlGenerator.GetSelectAll();
            return await Connection.QueryAsync<TEntity>(sql);
        }

        /// <inheritdoc />
        /// <summary>
        /// GetManyAsync
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetManyAsync(object filters)
        {
            var sql = SqlGenerator.GetSelect(filters);
            return await Connection.QueryAsync<TEntity>(sql, filters);
        }

        /// <inheritdoc />
        /// <summary>
        /// GetByScalarValueAsync
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByScalarValueAsync(object filters)
        {
            var sql = SqlGenerator.GetSelect(filters, 1);
            return await Connection.QueryFirstOrDefaultAsync<TEntity>(sql, filters);
        }

        /// <summary>
        /// Exists Async
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistsAsync(object filters)
        {
            var sql = SqlGenerator.GetSelect(filters, 1);
            return await Connection.ExecuteScalarAsync<int>(sql, filters) > 0;
        }

        /// <inheritdoc />
        /// <summary>
        /// Add Async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
                var sql = SqlGenerator.GetInsert();
            if (SqlGenerator.IsIdentity)
            {
                // exec 1
                var newId = await Connection.QuerySingleAsync<decimal>(sql, entity);
                if (newId > 0)
                {
                    var newParsedId = Convert.ChangeType(newId, SqlGenerator.IdentityProperty.PropertyInfo.PropertyType);
                    SqlGenerator.IdentityProperty.PropertyInfo.SetValue(entity, newParsedId);
                    return entity;
                }
                else
                    throw new DataException($"The action to add a new {typeof(TEntity).Name} was unsuccessful. Data for {typeof(TEntity).Name} Object is invalid");
            }
            else
            {
                // exec 2
                if (Connection.Execute(sql, entity) > 0)
                    return entity;
                else
                    throw new DataException($"The action to add a new {typeof(TEntity).Name} was unsuccessful. Data for {typeof(TEntity).Name} Object is invalid");
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
                var sql = SqlGenerator.GetUpdate();
            if (await Connection.ExecuteAsync(sql, entity) > 0)
                return true;
            else
                throw new DataException($"The action to update an existing {typeof(TEntity).Name} was unsuccessful. Data for {typeof(TEntity).Name} Object is invalid.");
            
        }

        /// <inheritdoc />
        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(object key)
        {
            var sql = SqlGenerator.GetDelete();

            if (await Connection.ExecuteAsync(sql, key) > 0)
                return true;
            else
                throw new DataException($"The action to Delete an existing {typeof(TEntity).Name} was Unsuccessful.");
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Gets the exceptions.
        /// </summary>
        /// <param name="rootException">The root exception.</param>
        /// <returns>Returns a list of exceptions</returns>
        protected IList<string> GetExceptions(Exception rootException)
        {
            IList<string> exceptions = new List<string>();

            if (rootException == null)
            {
                return exceptions;
            }

            var exception = rootException;
            exceptions.Add(exception.Message);

            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                exceptions.Add(exception.Message);
            }

            return exceptions;
        }

        #endregion
    }
}
