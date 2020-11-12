namespace Appointments.Utilities.SqlGenerator
{
    using Attributes;
    using System.Reflection;

    /// <summary>
    /// 
    /// </summary>
    public class PropertyMetadata
    {
        public PropertyInfo PropertyInfo { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Alias { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string ColumnName => string.IsNullOrEmpty(Alias) ? PropertyInfo.Name : Alias;

        /// <summary>
        /// 
        /// </summary>
        public string Name => PropertyInfo.Name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        public PropertyMetadata(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;

            var alias = PropertyInfo.GetCustomAttribute<StoredAs>();
            Alias = alias != null ? alias.Value : string.Empty;
        }
    }
}
