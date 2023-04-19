using System.Security.Claims;
using webApiTesis.Services.IServices;

namespace webApiTesis.Services
{
    public class ServicioSecurity : ISecurityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServicioSecurity(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? null;
        }

        public bool CheckUserHasroles(string[] roles)
        {
            var userRoles = (_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty).Split(",").ToList();

            return userRoles.Any() && userRoles.Any(x => roles.Contains(x));
        }

        public int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
