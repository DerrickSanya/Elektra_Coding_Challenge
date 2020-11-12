namespace Appointments.Utilities.SqlGenerator.Attributes
{
    using System;

    /// <summary>
    /// FullTextSearchColumn
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public sealed class FullTextSearchColumn : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        public FullTextSearchColumn(string value)
        {
            Value = value;
        }
    }
}
