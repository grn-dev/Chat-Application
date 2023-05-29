using System;
using System.Security.Claims;
using Chat.Infra.Security.Security;

namespace Chat.Infra.Security.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
           //return new Guid("5C99E5A3-C463-45CA-9338-FE4B92243C45");
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            if (principal.FindFirst(CustomClaims.UserId) == null)
                return Guid.Empty;
            return new Guid(principal.FindFirst(CustomClaims.UserId).Value);
        }


        public static int GetUserApplicatiopnId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return int.Parse(principal.FindFirst(CustomClaims.ApplicationId)?.Value);
        }

        public static int GetUserTenantId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return int.Parse(principal.FindFirst(CustomClaims.TenantId)?.Value);
        }

        public static string GetUserNationalCode(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(CustomClaims.NationalCode)?.Value;
        }
    }
}