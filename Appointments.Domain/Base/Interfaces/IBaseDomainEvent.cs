namespace Appointments.Domain.Base.Interfaces
{
    using System;
    using MediatR;

    /// <summary>
    /// IBaseDomainEvent
    /// </summary>
    public interface IBaseDomainEvent : INotification
    {
        /// <summary>
        /// CreatedOn
        /// </summary>
        DateTime CreatedOn { get; }
    }
}