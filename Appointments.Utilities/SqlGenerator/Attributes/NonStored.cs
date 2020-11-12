namespace Appointments.Utilities.SqlGenerator.Attributes
{
    using System;

    /// <summary>
    /// NonStored attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class NonStored : Attribute
    {
    }
}
