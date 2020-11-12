namespace Appointments.API.Helpers
{
    using System;
    using System.Linq;
    using Appointments.Application.Configuration;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Execution Context Accessor
    /// </summary>
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        /// <summary>
        /// IHttpContextAccessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Execution Context Accessor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// CorrelationId
        /// </summary>
        public Guid CorrelationId
        {
            get
            {
                if (IsAvailable && _httpContextAccessor.HttpContext.Request.Headers.Keys.Any(x => x == ConcurrencyMiddleware.HeaderKey))
                    return Guid.Parse(_httpContextAccessor.HttpContext.Request.Headers[ConcurrencyMiddleware.HeaderKey]);

                throw new ApplicationException("Http context and correlation id is not available");
            }
        }

        public bool IsAvailable => _httpContextAccessor.HttpContext != null;
    }
}
