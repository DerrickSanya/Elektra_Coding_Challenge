namespace Appointments.Infrastructure.Helpers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Invalid Command Problem Details
    /// </summary>
    public class InvalidCommandProblemDetails : ProblemDetails
    {
        /// <summary>
        /// Invalid Command Problem Details
        /// </summary>
        /// <param name="exception"></param>
        public InvalidCommandProblemDetails(InvalidCommandException exception)
        {
            Title = exception.Message;
            Status = StatusCodes.Status400BadRequest;
            Detail = exception.ExceptionMessage;
            Type = "https://elektra.appointments.api/validation-error";
        }
    }
}
