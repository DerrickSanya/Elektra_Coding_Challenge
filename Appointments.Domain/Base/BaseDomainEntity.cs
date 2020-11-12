namespace Appointments.Domain.Base
{
    using System.Collections.Generic;
    using Appointments.Domain.Base.Exceptions;
    using Appointments.Domain.Base.Interfaces;
    
    /// <summary>
    /// Base Domain Entity
    /// </summary>
    public abstract class BaseDomainEntity
    {
        /// <summary>
        /// Domain Events
        /// </summary>
        private List<IBaseDomainEvent> domainEvents;

        /// <summary>
        /// Domain events occurred.
        /// </summary>
        public IReadOnlyCollection<IBaseDomainEvent> DomainEvents => domainEvents?.AsReadOnly();

        /// <summary>
        /// Add domain event.
        /// </summary>
        /// <param name="domainEvent"></param>
        protected void AddDomainEvent(IBaseDomainEvent domainEvent)
        {
            domainEvents ??= new List<IBaseDomainEvent>();
            domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Clear domain events.
        /// </summary>
        public void ClearDomainEvents()
        {
            domainEvents?.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessRule"></param>
        protected static void ValidateBusinessRule(IBusinessRule businessRule)
        {
            if (businessRule.IsViolated())
            {
                throw new BusinessRuleViolationException(businessRule);
            }
        }
    }
}
