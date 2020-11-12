namespace Appointments.Utilities.SqlGenerator.Attributes
{
    using System;

    /// <inheritdoc />
    /// <summary>
    /// KeyProperty Attribute
    /// </summary>
    public sealed class KeyProperty : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Identity { get; set; }
    }
}
