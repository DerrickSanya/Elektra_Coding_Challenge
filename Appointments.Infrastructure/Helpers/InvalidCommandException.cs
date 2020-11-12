namespace Appointments.Infrastructure.Helpers
{
    using System;

    /// <summary>
    /// Invalid Command Exception
    /// </summary>
    public class InvalidCommandException : Exception
    {
        /// <summary>
        /// Details
        /// </summary>
        public string ExceptionMessage { get; }

        /// <summary>
        /// InvalidCommandException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exceptionDetails"></param>
        public InvalidCommandException(string message, string exceptionDetails) : base(message)
        {
            ExceptionMessage = exceptionDetails;
        }
    }
}
