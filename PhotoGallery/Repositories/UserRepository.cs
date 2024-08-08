using Microsoft.EntityFrameworkCore;
using PhotoGallery.Api.Host.Data;
using PhotoGallery.Api.Host.Repositories.Interfaces;
using PhotoGallery.Api.Models.Entities;

namespace PhotoGallery.Api.Host.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserAsync(string email, string password)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password == password) ?? null;
        }
    }
}
