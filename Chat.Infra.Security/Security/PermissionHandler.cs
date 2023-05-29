using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace Chat.Infra.Security.Security
{
    public class PermissionHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {

            if (!context.User.Identity.IsAuthenticated)
                return Task.CompletedTask;
            var pendingRequirements = context.PendingRequirements.ToList();

            foreach (var requirement in pendingRequirements)
            {
                if (requirement is ReadPermission)
                {
                    if (hasPermisson(context.User, context.Resource))
                    {
                        //context.Succeed(requirement);
                    }

                }

                context.Succeed(requirement);
            }

            //TODO: Use the following if targeting a version of
            //.NET Framework older than 4.6:
            //      return Task.FromResult(0);
            return Task.CompletedTask;
        }

        private bool hasPermisson(ClaimsPrincipal user, object resource)
        {
            var UserRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(claim => new { claim.Value }).ToList();
            // Code omitted for brevity
            var RouteEndpoint = (RouteEndpoint)resource;

            var controller = RouteEndpoint.RoutePattern.Defaults["controller"];
            var action = RouteEndpoint.RoutePattern.Defaults["action"];
            //to do check role 
            return true;
        }


    }
}