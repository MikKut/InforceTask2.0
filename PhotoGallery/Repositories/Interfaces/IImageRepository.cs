using PhotoGallery.Api.Models.Entities;

namespace PhotoGallery.Api.Host.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAlbumsImagesAsync(Album album);
        Task AddImageAsync(Album album, Image image);
        Task DeleteImageAsync(Album album, Image imageToDelete);
        Task DislikeImageAsync(Album album, Image image);
        Task LikeImageAsync(Album album, Image image);
    }
}