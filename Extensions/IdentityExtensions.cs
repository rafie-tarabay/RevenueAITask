using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RevenueAI.Extensions
{
    public static class IdentityExtensions
    {
        public static bool HasPermission(this ClaimsPrincipal principal, string entity, string operation)
        {
            return principal.Claims.Any(c => (c.Type == entity && c.Value == operation) || c.Type == "*");
        }

        public static bool HasManyPermission(this ClaimsPrincipal principal, string entity)
        {
            return principal.Claims.Count(c => c.Type == entity) > 0;
        }

    }
}
