using Microsoft.AspNetCore.Authorization;

namespace PhotoGallery.Api.Host.Data.Authorization
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string UserRole { get; }

        public RoleRequirement(string userRole)
        {
            UserRole = userRole;
        }
    }

}
