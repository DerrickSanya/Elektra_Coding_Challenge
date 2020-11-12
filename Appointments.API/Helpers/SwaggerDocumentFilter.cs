using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
namespace Appointments.API.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// Swagger Document Filter
    /// </summary>
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IServiceProvider _provider;

        /// <summary>
        /// SwaggerDocumentFilter
        /// </summary>
        /// <param name="provider"></param>
        public SwaggerDocumentFilter(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var http = _provider.GetRequiredService<IHttpContextAccessor>();
            var auth = _provider.GetRequiredService<IAuthorizationService>();

            foreach (var description in context.ApiDescriptions)
            {
                var authAttributes = description.CustomAttributes().OfType<AuthorizeAttribute>();
                bool notShowen = IsAnonymousForbiden(http, authAttributes) || IsPolicyForbiden(http, auth, authAttributes);

                if (!notShowen)
                    continue; // user passed all permissions checks

                var route = "/" + description.RelativePath.TrimEnd('/');
                var path = swaggerDoc.Paths[route];

                // remove method or entire path (if there are no more methods in this path)
                OperationType operation = Enum.Parse<OperationType>(description.HttpMethod, true);
                path.Operations.Remove(operation);
                if (path.Operations.Count == 0)
                {
                    swaggerDoc.Paths.Remove(route);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="http"></param>
        /// <param name="auth"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private static bool IsPolicyForbiden(IHttpContextAccessor http, IAuthorizationService auth, IEnumerable<AuthorizeAttribute> attributes)
        {
            var policies = attributes.Where(p => !String.IsNullOrEmpty(p.Policy)).Select(a => a.Policy).Distinct();
            var result = Task.WhenAll(policies.Select(p => auth.AuthorizeAsync(http.HttpContext.User, p))).Result;
            return result.Any(r => !r.Succeeded);
        }

        /// <summary>
        /// IsAnonymousForbiden
        /// </summary>
        /// <param name="http"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private static bool IsAnonymousForbiden(IHttpContextAccessor http, IEnumerable<AuthorizeAttribute> attributes)
        {
            return attributes.Any() && !http.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
