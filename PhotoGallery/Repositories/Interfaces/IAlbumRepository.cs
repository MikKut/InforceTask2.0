using Catalog.Host.Data;
using PhotoGallery.Api.Models.DTO;
using PhotoGallery.Api.Models.Entities;

namespace PhotoGallery.Api.Host.Repositories
{
    public interface IAlbumRepository
    {
        Task<PaginatedItems<Album>> GetAlbumsByPageAsync(int pageIndex, int pageSize);
        Task<IEnumerable<Album>> GetAlbumsByUserWithImagesAsync(int userId, int quantityOfImages);
        Task<IEnumerable<Album>> GetAlbumsAsync();
        Task AddAlbumAsync(Album addItem);
        Task DeleteAlbumAsync(Album deleteItem);
        Task<int> GetAlbumIdAsync(AlbumDto album);
        Task<PaginatedItems<Image>> GetImagesByPageAsync(int pageIndex, int pageSize, AlbumDto album);
        Task<bool> UpdateAlbumAsync(Album oldAlbum, Album newAlbum);
    }
}