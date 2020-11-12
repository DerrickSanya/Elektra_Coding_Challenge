namespace Appointments.Application.Configuration
{
    using System;

    /// <summary>
    /// IExecutionContextAccessor
    /// </summary>
    public interface IExecutionContextAccessor
    {
        /// <summary>
        /// CorrelationId
        /// </summary>
        Guid CorrelationId { get; }

        /// <summary>
        /// IsAvailable
        /// </summary>
        bool IsAvailable { get; }
    }
}
