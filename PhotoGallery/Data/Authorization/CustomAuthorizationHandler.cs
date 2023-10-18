using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PhotoGallery.Api.Host.Data.Authorization;
public class CustomAuthorizationHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        // Check if the user has the specified role (UserRole) in their claims
        if (context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == requirement.UserRole))
        {
            context.Succeed(requirement); // Succeed the requirement if authorized
        }

        return Task.CompletedTask;
    }
}
