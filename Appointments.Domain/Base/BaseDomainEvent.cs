namespace Appointments.Domain.Base
{
    using System;
    using Appointments.Domain.Base.Interfaces;

    /// <summary>
    /// Base Domain Event
    /// </summary>
    public class BaseDomainEvent : IBaseDomainEvent
    {
        /// <summary>
        /// Base Domain Event
        /// </summary>
        public BaseDomainEvent()
        {
            this.CreatedOn = DateTime.Now;
        }

        /// <summary>
        /// CreatedOn
        /// </summary>
        public DateTime CreatedOn { get; }
    }
}
