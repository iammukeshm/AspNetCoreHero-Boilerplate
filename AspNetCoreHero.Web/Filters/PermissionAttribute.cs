using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Filters
{
    public class PermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private string[] _permission;
        public PermissionAttribute(params string[] permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                return;
            }
            var allClaims = user.Claims.Select(a=>a.Value).ToList();
            var success = allClaims.Intersect(_permission).Any();
            if (!success)
            {
                throw new Exception("Oops!");
            }
            return;
        }
    }
}
