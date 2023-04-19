using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace webApiTesis.Filters.Seguridad
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizeAttribute:Attribute, IAuthorizationFilter
    {

        private readonly IList<string> _roles;

        public AuthorizeAttribute(params string[] roles)
        {
            _roles = roles ?? new string[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userName = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization
            var role = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role == null || !_roles.Contains(role))
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Nivel de accesso insuficiente." }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }


    }
}
