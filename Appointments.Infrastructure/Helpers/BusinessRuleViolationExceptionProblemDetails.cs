namespace Appointments.Infrastructure.Helpers
{
    using Appointments.Domain.Base.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Business Rule Violation Exception Problem Details
    /// </summary>
    public class BusinessRuleViolationExceptionProblemDetails : ProblemDetails
    {
        /// <summary>
        /// BusinessRuleViolationExceptionProblemDetails
        /// </summary>
        /// <param name="exception"></param>
        public BusinessRuleViolationExceptionProblemDetails(BusinessRuleViolationException exception)
        {
            this.Title = "Business rule validation error";
            this.Status = StatusCodes.Status409Conflict;
            this.Detail = exception.ExceptionMessage;
            this.Type = "https://elektra.appointments.api/business-rule-validation-error";
        }
    }
}
