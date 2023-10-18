using PhotoGallery.Api.Models.DTO;
using PhotoGallery.Api.Models.Entities;

namespace PhotoGallery.Api.Host.Services
{
    public interface IImageService
    {
        Task<IEnumerable<ImageDto>> GetAlbumsImagesAsync(AlbumDto album);
        Task AddImageAsync(AlbumDto album, ImageDto image);
        Task DeleteImageAsync(AlbumDto album, ImageDto imageToDeleteDto);
        Task UpdateImageLikeAsync(AlbumDto album, ImageDto imageToUpdateDto, bool isLike);
    }
}