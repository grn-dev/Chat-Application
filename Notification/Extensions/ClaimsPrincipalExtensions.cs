using System;
using System.Security.Claims;

namespace Chat.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            //return new Guid("54F0F7C2-E701-4361-A20A-9F79BA08D1BB"); 
            //return new Guid("FB23AC30-166C-4A00-B118-1C4A16979CA6"); 
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