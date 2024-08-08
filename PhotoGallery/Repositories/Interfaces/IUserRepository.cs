using PhotoGallery.Api.Models.Entities;

namespace PhotoGallery.Api.Host.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(string email, string password);
    }
}