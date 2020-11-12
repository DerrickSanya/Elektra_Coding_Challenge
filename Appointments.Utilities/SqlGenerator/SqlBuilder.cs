namespace Appointments.Utilities.SqlGenerator
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Appointments.Utilities.SqlGenerator.Attributes;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SqlBuilder<TEntity> : ISqlBuilder<TEntity>
    {
        #region Properties

        /// <inheritdoc />
        /// <summary>
        /// IsIdentity
        /// </summary>
        public bool IsIdentity => IdentityProperty != null;

        /// <inheritdoc />
        /// <summary>
        /// LogicalDelete
        /// </summary>
        public bool LogicalDelete => StatusProperty != null;

        /// <inheritdoc />
        /// <summary>
        /// TableName
        /// </summary>
        public string TableName { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// IdentityProperty
        /// </summary>
        public string Scheme { get; private set; }

        /// <summary>
        /// Gets the full text search column.
        /// </summary>
        /// <value>
        /// The full text search column.
        /// </value>
        public string FullTextSearchColumn { get; private set; }

        /// <summary>
        /// Gets the search properties.
        /// </summary>
        /// <value>
        /// The search properties.
        /// </value>
        public IEnumerable<PropertyMetadata> SearchColumnProperties { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public PropertyMetadata IdentityProperty { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// KeyProperties
        /// </summary>
        public IEnumerable<PropertyMetadata> KeyProperties { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// BaseProperties
        /// </summary>
        public IEnumerable<PropertyMetadata> BaseProperties { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// StatusProperty
        /// </summary>
        public PropertyMetadata StatusProperty { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// LogicalDeleteValue
        /// </summary>
        public object LogicalDeleteValue { get; private set; }

        /// <summary>
        /// Gets the name of the temporary table.
        /// </summary>
        /// <value>
        /// The name of the temporary table.
        /// </value>
        public string TempTableName { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// SqlBuilder
        /// </summary>
        public SqlBuilder()
        {
            LoadEntityMetadata();
        }

        /// <summary>
        /// LoadEntityMetadata
        /// </summary>
        private void LoadEntityMetadata()
        {
            var entityType = typeof(TEntity);

            var aliasAttribute = entityType.GetCustomAttribute<StoredAs>();
            var schemeAttribute = entityType.GetCustomAttribute<Scheme>();
            var fullTextSearchColumn = entityType.GetCustomAttribute<FullTextSearchColumn>();
            TableName = aliasAttribute != null ? aliasAttribute.Value : entityType.Name;
            TempTableName = $"#{TableName}TempTbl";
            Scheme = schemeAttribute != null ? schemeAttribute.Value : "dbo";
            FullTextSearchColumn = fullTextSearchColumn != null ? fullTextSearchColumn.Value : entityType.Name;

            //Load all the "primitive" entity properties
            var props = entityType.GetProperties().Where(p => p.PropertyType.IsValueType ||
                                                                                    p.PropertyType.Name.Equals("String", StringComparison.InvariantCultureIgnoreCase) ||
                                                                                    p.PropertyType.Name.Equals("Byte[]", StringComparison.InvariantCultureIgnoreCase));

            // Filter the non stored properties
            BaseProperties = props.Where(p => !p.GetCustomAttributes<NonStored>().Any()).Select(p => new PropertyMetadata(p));

            // Filter key properties
            KeyProperties = props.Where(p => p.GetCustomAttributes<KeyProperty>().Any()).Select(p => new PropertyMetadata(p));

            // get search columns
            SearchColumnProperties = props.Where(p => p.GetCustomAttributes<SearchableColumn>().Any()).Select(p => new PropertyMetadata(p));

            // Use identity as key pattern
            var identityProperty = props.SingleOrDefault(p => p.GetCustomAttributes<KeyProperty>().Any(k => k.Identity));
            IdentityProperty = identityProperty != null ? new PropertyMetadata(identityProperty) : null;

            // Status property (if exists, and if it does, it must be an enumeration)
            var statusProperty = props.FirstOrDefault(p => p.PropertyType.IsEnum && p.GetCustomAttributes<StatusProperty>().Any());

            if (statusProperty != null)
            {
                StatusProperty = new PropertyMetadata(statusProperty);
                var statusPropertyType = statusProperty.PropertyType;
                var deleteOption = statusPropertyType.GetFields().FirstOrDefault(f => f.GetCustomAttribute<Deleted>() != null);

                if (deleteOption != null)
                {
                    var enumValue = Enum.Parse(statusPropertyType, deleteOption.Name);

                    if (enumValue != null)
                        LogicalDeleteValue = Convert.ChangeType(enumValue, Enum.GetUnderlyingType(statusPropertyType));
                }
            }
        }

        #endregion

        #region Query generators

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public virtual string GetInsert()
        {
            // Enumerate the entity properties. Identity property (if exists) has to be ignored
            IEnumerable<PropertyMetadata> properties = (IsIdentity ?
                                                        BaseProperties.Where(p => !p.Name.Equals(IdentityProperty.Name, StringComparison.InvariantCultureIgnoreCase)) :
                                                        BaseProperties).ToList();

            var columNames = string.Join(", ", properties.Select(p => $"[{TableName}].[{p.ColumnName}]"));
            var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("INSERT INTO [{0}].[{1}] {2} {3} ",
                                    Scheme,
                                    TableName,
                                    string.IsNullOrEmpty(columNames) ? string.Empty : $"({columNames})",
                                    string.IsNullOrEmpty(values) ? string.Empty : $" VALUES ({values})");

            // If the entity has an identity key, we create a new variable into the query in order to retrieve the generated id
            if (IsIdentity)
            {
                sqlBuilder.AppendLine("DECLARE @NEWID NUMERIC(38, 0)");
                sqlBuilder.AppendLine("SET	@NEWID = SCOPE_IDENTITY()");
                sqlBuilder.AppendLine("SELECT @NEWID");
            }

            return sqlBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetUpdate()
        {
            var properties = BaseProperties.Where(p => !KeyProperties.Any(k => k.Name.Equals(p.Name, StringComparison.InvariantCultureIgnoreCase)));

            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("UPDATE [{0}].[{1}] SET {2} WHERE {3}",
                                    Scheme,
                                    TableName,
                                    string.Join(", ", properties.Select(p =>
                                        $"[{TableName}].[{p.ColumnName}] = @{p.Name}")),
                                    string.Join(" AND ", KeyProperties.Select(p =>
                                        $"[{TableName}].[{p.ColumnName}] = @{p.Name}")));

            return sqlBuilder.ToString();
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public virtual string GetSelectAll()
        {
            return GetSelect(new { });
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="rowCount">Maximum number of rows to return</param>
        /// <returns></returns>
        public virtual string GetSelect(object filters, int? rowCount = null)
        {
            string ProjectionFunction(PropertyMetadata p)
            {
                return !string.IsNullOrEmpty(p.Alias) ? $"[{TableName}].[{p.ColumnName}] AS [{p.Name}]" : $"[{TableName}].[{p.ColumnName}]";
            }

            var sqlBuilder = new StringBuilder();
            var rowLimitSql = string.Empty;

            if (rowCount.HasValue)
                rowLimitSql = $"TOP {rowCount} ";

            sqlBuilder.AppendFormat("SELECT {0}{1} FROM [{2}].[{3}] WITH (NOLOCK)",
                                    rowLimitSql,
                                    string.Join(", ", BaseProperties.Select(ProjectionFunction)),
                                    Scheme,
                                    TableName
                                    );

            // Properties of the dynamic filters object
            var filterProperties = filters.GetType().GetProperties().Select(p => p.Name);
            var containsFilter = (filterProperties.Any());

            if (containsFilter)
                sqlBuilder.AppendFormat(" WHERE {0} ", ToWhere(filterProperties, filters));

            // Evaluates if this repository implements logical delete
            if (!LogicalDelete)
                return sqlBuilder.ToString();

            sqlBuilder.AppendFormat(containsFilter ? " AND [{0}].[{1}] != {2}" : " WHERE [{0}].[{1}] != {2}", TableName, StatusProperty.Name, LogicalDeleteValue);
            return sqlBuilder.ToString();
        }

        /// <summary>
        /// Get Select Count
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public virtual string GetSelectCount(object filters)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat($"SELECT COUNT(*) FROM [{Scheme}].[{TableName}] WITH (NOLOCK)", Scheme, TableName);

            // Properties of the dynamic filters object
            var filterProperties = filters.GetType().GetProperties().Select(p => p.Name);
            var containsFilter = (filterProperties.Any());

            if (containsFilter)
                sqlBuilder.AppendFormat(" WHERE {0} ", ToWhere(filterProperties, filters));

            // Evaluates if this repository implements logical delete
            if (!LogicalDelete)
                return sqlBuilder.ToString();

            sqlBuilder.AppendFormat(containsFilter ? " AND [{0}].[{1}] != {2}" : " WHERE [{0}].[{1}] != {2}", TableName, StatusProperty.Name, LogicalDeleteValue);
            return sqlBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetDelete()
        {
            var sqlBuilder = new StringBuilder();

            if (!LogicalDelete)
            {
                sqlBuilder.AppendFormat("DELETE FROM [{0}].[{1}] WHERE {2}",
                                        Scheme,
                                        TableName,
                                        string.Join(" AND ", KeyProperties.Select(p => $"[{TableName}].[{p.ColumnName}] = @{p.Name}")));

            }
            else
                sqlBuilder.AppendFormat("UPDATE [{0}].[{1}] SET {2} WHERE {3}",
                                    Scheme,
                                    TableName,
                                    $"[{TableName}].[{StatusProperty.ColumnName}] = {LogicalDeleteValue}",
                                    string.Join(" AND ", KeyProperties.Select(p =>
                                        $"[{TableName}].[{p.ColumnName}] = @{p.Name}")));


            return sqlBuilder.ToString();
        }

        /// <summary>
        /// Gets the bulk update.
        /// </summary>
        /// <returns></returns>
        public virtual string GetBulkUpdate()
        {
            // TODO: Include Alias
            var properties = BaseProperties.Where(p => !KeyProperties.Any(k => k.Name.Equals(p.Name, StringComparison.InvariantCultureIgnoreCase)));

            string ProjectionFunction(PropertyMetadata p)
            {
                return $"[DEST].[{p.ColumnName}] = [T].[{p.ColumnName}]";
            }

            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("UPDATE DEST SET {0} \n FROM [{1}].[{2}] AS DEST INNER JOIN [dbo].[{3}] AS T ON {4}; \n IF OBJECT_ID('tempdb..{5}') IS NOT NULL \n DROP TABLE [dbo].[{6}];",
                                    string.Join(", ", properties.Select(ProjectionFunction)),
                                    Scheme,
                                    TableName,
                                    TempTableName,
                                    string.Join(", ", KeyProperties.Select(ProjectionFunction)),
                                    TempTableName,
                                    TempTableName);

            return sqlBuilder.ToString();
        }

        /// <summary>
        /// Creates the temporary table.
        /// </summary>
        /// <returns></returns>
        public virtual string CreateTempTable()
        {
            var properties = BaseProperties.Where(p => !KeyProperties.Any(k => k.Name.Equals(p.Name, StringComparison.InvariantCultureIgnoreCase)));

            string ProjectionFunction(PropertyMetadata p)
            {
                return $"[{p.ColumnName}]  {GetSqlDataType(p.PropertyInfo.PropertyType)}";
            }

            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("CREATE TABLE [dbo].[{0}]({1}{2}{3});",
                 TempTableName,
                 string.Join(", ", KeyProperties.Select(ProjectionFunction)),
                 KeyProperties.Count() > 1 ? " " : ", ",
                 string.Join(", ", properties.Select(ProjectionFunction)));

            return sqlBuilder.ToString();
        }

        #endregion

        #region Private Utility

        /// <summary>
        /// ToWhere
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public virtual string ToWhere(IEnumerable<string> properties, object filters)
        {
            return string.Join(" AND ", properties.Select(p =>
            {

                var propertyMetadata = BaseProperties.FirstOrDefault(pm => pm.Name.Equals(p, StringComparison.InvariantCultureIgnoreCase));
                var columnName = p;
                var propertyName = p;

                if (propertyMetadata != null)
                {
                    columnName = propertyMetadata.ColumnName;
                    propertyName = propertyMetadata.Name;
                }

                var prop = filters.GetType().GetProperty(propertyMetadata?.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                var values = prop.GetValue(filters, null);

                if (values == null)
                {
                    return $"[{TableName}].[{columnName}] IS NULL";
                }

                if ((values as IEnumerable) != null && !(values is string))
                {
                    return $"[{TableName}].[{columnName}] IN @{propertyName}";
                }

                return $"[{TableName}].[{columnName}] = @{propertyName}";

            }));
        }

        /// <summary>
        /// Gets the type of the SQL data.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="isPrimary">if set to <c>true</c> [is primary].</param>
        /// <returns></returns>
        private string GetSqlDataType(Type type, bool isPrimary = false)
        {
            var sqlType = new StringBuilder();
            var isNullable = false;
            if (Nullable.GetUnderlyingType(type) != null)
            {
                isNullable = true;
                type = Nullable.GetUnderlyingType(type);
            }
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    isNullable = true;
                    sqlType.Append("NVARCHAR(MAX)");
                    break;
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Int16:
                    sqlType.Append("INT");
                    break;
                case TypeCode.Boolean:
                    sqlType.Append("BIT");
                    break;
                case TypeCode.DateTime:
                    sqlType.Append("DATETIME");
                    break;
                case TypeCode.Decimal:
                case TypeCode.Double:
                    sqlType.Append("DECIMAL");
                    break;
            }
            if (!isNullable || isPrimary)
            {
                sqlType.Append(" NOT NULL");
            }
            return sqlType.ToString();
        }

        #endregion
    }
}