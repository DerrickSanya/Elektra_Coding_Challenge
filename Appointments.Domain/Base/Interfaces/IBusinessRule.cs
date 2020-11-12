namespace Appointments.Domain.Base.Interfaces
{
    public interface IBusinessRule
    {
        /// <summary>
        /// RuleName
        /// </summary>
        string RuleName { get; }

        /// <summary>
        /// Defines if the business rule has been violated
        /// </summary>
        /// <returns></returns>
        bool IsViolated();

        /// <summary>
        /// Violation Message
        /// </summary>
        string ViolationMessage { get; }
    }
}
