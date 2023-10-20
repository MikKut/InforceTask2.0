using PhotoGallery.Api.Models.DTO;
using PhotoGallery.Api.Models.Requests;
using PhotoGallery.Api.Models.Responses;

namespace PhotoGallery.Api.Host.Services.Interfaces
{
    public interface IAlbumService
    {
        Task<PaginatedItemsResponse<AlbumWithImageDto>> GetPaginatedAlbumsAsync(int pageSize, int pageIndex);
        Task<IEnumerable<AlbumWithImageDto>> GetUsersAlbumsWithFirstImageAsync(string jwtToken);
        Task AddAlbumAsync(AlbumDto addItem);
        Task DeleteAlbumAsync(AlbumDto deleteItem);
        Task<PaginatedItemsResponse<ImageDto>> GetPaginatedImagesAsync(int pageSize, int pageIndex, int albumId);
        Task UpdateAlbumAsync(AlbumDto oldItem, AlbumDto newItem);
    }
}