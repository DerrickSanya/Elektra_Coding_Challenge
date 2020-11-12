namespace  Appointments.Utilities.SqlGenerator.Attributes
{
    using System;

    /// <inheritdoc />
    /// <summary>
    /// Scheme Attribute
    /// </summary>
    public sealed class Scheme : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        public Scheme(string value)
        {
            Value = value;
        }
    }
}
