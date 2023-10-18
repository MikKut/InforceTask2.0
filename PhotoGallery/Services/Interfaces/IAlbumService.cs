using PhotoGallery.Api.Models.DTO;
using PhotoGallery.Api.Models.Requests;
using PhotoGallery.Api.Models.Responses;

namespace PhotoGallery.Api.Host.Services
{
    public interface IAlbumService
    {
        Task<PaginatedItemsResponse<AlbumDto>> GetPaginatedAlbumsAsync(int pageSize, int pageIndex);
        Task<IEnumerable<AlbumWithImageDto>> GetUsersAlbumsWithFirstImageAsync(string jwtToken);
        Task AddAlbumAsync(AlbumDto addItem);
        Task DeleteAlbumAsync(AlbumDto deleteItem);
        Task<PaginatedItemsResponse<ImageDto>> GetPaginatedImagesAsync(int pageSize, int pageIndex, AlbumDto album);
        Task UpdateAlbumAsync(AlbumDto oldItem, AlbumDto newItem);
    }
}