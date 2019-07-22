using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FH.Swagger
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {

            bool hasAuthorize = context.MethodInfo.GetCustomAttributes().OfType<AuthorizeAttribute>().Any();
            if (!hasAuthorize)
            {
                hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes().OfType<AuthorizeAttribute>().Any() ||
                               context.MethodInfo.ReflectedType.GetCustomAttributes().OfType<AuthorizeAttribute>().Any();
            }
            if (hasAuthorize)
            {
                operation.Responses.Add("401", new Response { Description = "Unauthorized" });
                operation.Responses.Add("403", new Response { Description = "Forbidden" });

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
                operation.Security.Add(new Dictionary<string, IEnumerable<string>> {
                        { "oauth2", new[] { "api" } }
                });
            }


        }
    }
}
