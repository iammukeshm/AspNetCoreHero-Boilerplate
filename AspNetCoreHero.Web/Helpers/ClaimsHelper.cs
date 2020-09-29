using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Helpers
{
    public static class ClaimsHelper
    {
        public static void Check(this ClaimsPrincipal claimsPrincipal,IEnumerable<string> permissions )
        {
            if (!claimsPrincipal.Identity.IsAuthenticated)
            {
                return;
            }
            var allClaims = claimsPrincipal.Claims.Select(a => a.Value).ToList();
            var success = allClaims.Intersect(permissions).Any();
            if (!success)
            {
                throw new Exception();
            }
            return;
        }
    }
}
