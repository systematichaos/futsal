using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace FutsalSutsal.Identity
{
    public static class IdentityExtensionMethods
    {
        /// <summary>
        /// Check the IsUserBeingImpersonated claim to see if it is true and if so, return that they are impersonating
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static bool IsImpersonating(this IPrincipal principal)
        {
            var claimsPrincipal = (ClaimsPrincipal)principal;
            if (claimsPrincipal != null)
                return claimsPrincipal.HasClaim("IsUserBeingImpersonated", true.ToString());

            return false;
        }

        /// <summary>
        /// Get the claim with the original user id
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetOriginalID(this IPrincipal principal)
        {
            var claimsPrincipal = (ClaimsPrincipal)principal;
            if (principal.IsImpersonating())
            {
                return claimsPrincipal.Claims.Single(x => x.Type == "OriginalUserID").Value;
            }

            return "";
        }


        public static string GetUserRole(this IPrincipal principal)
        {
            var claimsPrincipal = (ClaimsPrincipal)principal;
            return claimsPrincipal.Claims.Single(x => x.Type == ClaimTypes.Role).Value;
        }

        public static IEnumerable<string> GetUserRoles(this IPrincipal principal)
        {
            var claimsPrincipal = (ClaimsPrincipal)principal;
            var sth = claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
            return sth;
        }

    }
}