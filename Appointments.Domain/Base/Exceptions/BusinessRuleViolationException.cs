namespace Appointments.Domain.Base.Exceptions
{
    using System;
    using Appointments.Domain.Base.Interfaces;

    /// <summary>
    /// Business Rule Violation Exception
    /// </summary>
    public class BusinessRuleViolationException : Exception
    {
        /// <summary>
        /// Business Rule Interface
        /// </summary>
        public IBusinessRule BusinessRule { get; }

        /// <summary>
        /// Exception Message
        /// </summary>
        public string ExceptionMessage { get; }

        /// <summary>
        /// BusinessRuleViolationException
        /// </summary>
        /// <param name="businessRule"></param>
        public BusinessRuleViolationException(IBusinessRule businessRule) : base(businessRule.ViolationMessage)
        {
            BusinessRule = businessRule;
            this.ExceptionMessage = businessRule.ViolationMessage;
        }

        /// <summary>
        /// Exception to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{BusinessRule.RuleName}: {BusinessRule.ViolationMessage}";
        }
    }
}
