using PhotoGallery.Api.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PhotoGallery.Api.Host.Data.Authorization
{
    public interface IJwtHandler
    {
        Task<JwtSecurityToken> GetTokenAsync(User user);
        public ClaimsPrincipal? GetTokenPrincipalsOrNull(string token);
    }
}