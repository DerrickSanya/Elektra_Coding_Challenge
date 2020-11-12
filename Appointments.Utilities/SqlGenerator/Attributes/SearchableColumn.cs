namespace Appointments.Utilities.SqlGenerator.Attributes
{
    using System;

    /// <summary>
    /// SearchableColumn
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public sealed class SearchableColumn : Attribute
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        public SearchableColumn(string value)
        {
            Value = value;
        }
    }
}
