namespace Appointments.Utilities.SqlGenerator
{
    using System.Collections.Generic;

    /// <summary>
    /// ISqlBuilder
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISqlBuilder<TEntity>
    {

        #region Properties

        /// <summary>
        /// IsIdentity
        /// </summary>
        bool IsIdentity { get; }

        /// <summary>
        /// IdentityProperty
        /// </summary>
        PropertyMetadata IdentityProperty { get; }

        /// <summary>
        /// TableName
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// Scheme
        /// </summary>
        string Scheme { get; }

        /// <summary>
        /// Gets the search properties.
        /// </summary>
        /// <value>
        /// The search properties.
        /// </value>
        IEnumerable<PropertyMetadata> SearchColumnProperties { get; }

        /// <summary>
        /// KeyProperties
        /// </summary>
        IEnumerable<PropertyMetadata> KeyProperties { get; }

        /// <summary>
        /// BaseProperties
        /// </summary>
        IEnumerable<PropertyMetadata> BaseProperties { get; }

        /// <summary>
        /// StatusProperty
        /// </summary>
        PropertyMetadata StatusProperty { get; }

        /// <summary>
        /// LogicalDeleteValue
        /// </summary>
        object LogicalDeleteValue { get; }

        /// <summary>
        /// LogicalDelete
        /// </summary>
        bool LogicalDelete { get; }

        /// <summary>
        /// Gets the name of the temporary table.
        /// </summary>
        /// <value>
        /// The name of the temporary table.
        /// </value>
        public string TempTableName { get; }

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetSelectAll();

        /// <summary>
        /// GetSelect
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        string GetSelect(object filters, int? rowCount = null);

        /// <summary>
        /// GetSelectCount
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        string GetSelectCount(object filters);

        /// <summary>
        /// GetInsert
        /// </summary>
        /// <returns></returns>
        string GetInsert();

        /// <summary>
        /// GetUpdate
        /// </summary>
        /// <returns></returns>
        string GetUpdate();

        /// <summary>
        /// GetDelete
        /// </summary>
        /// <returns></returns>
        string GetDelete();

        /// <summary>
        /// Creates the temporary table.
        /// </summary>
        /// <returns></returns>
        string CreateTempTable();

        /// <summary>
        /// Gets the bulk update.
        /// </summary>
        /// <returns></returns>
        string GetBulkUpdate();

        #endregion

        #region Private Utility      

        /// <summary>
        /// Converts to where.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        string ToWhere(IEnumerable<string> properties, object filters);
        #endregion
    }
}