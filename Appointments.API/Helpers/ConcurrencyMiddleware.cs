namespace Appointments.API.Helpers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Concurrency Middleware
    /// </summary>
    internal class ConcurrencyMiddleware
    {
        /// <summary>
        /// headerKey
        /// </summary>
        internal const string HeaderKey = "CorrelationId";

        /// <summary>
        /// Request Delegate
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Concurrency Middleware
        /// </summary>
        /// <param name="next"></param>
        public ConcurrencyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (context.Request != null)
                context.Request.Headers.Add(HeaderKey, Guid.NewGuid().ToString());

            await _next.Invoke(context);
        }
    }
}
