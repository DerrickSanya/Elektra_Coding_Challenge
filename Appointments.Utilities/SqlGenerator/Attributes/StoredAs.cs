namespace Appointments.Utilities.SqlGenerator.Attributes
{
    using System;

    /// <summary>
    /// StoredAs Attribute
    /// </summary>
    public sealed class StoredAs : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public StoredAs(string value)
        {
            Value = value;
        }
    }
}
