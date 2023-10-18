using PhotoGallery.Api.Models.Requests;
using PhotoGallery.Api.Models.Responses;

namespace PhotoGallery.Api.Host.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}