using AutoMapper;
using Catalog.Host.Data;
using Infrastructure;
using PhotoGallery.Api.Host.Data;
using PhotoGallery.Api.Host.Data.Authorization;
using PhotoGallery.Api.Host.Repositories;
using PhotoGallery.Api.Models.DTO;
using PhotoGallery.Api.Models.Entities;
using PhotoGallery.Api.Models.Requests;
using PhotoGallery.Api.Models.Responses;
using System.Security.Claims;

namespace PhotoGallery.Api.Host.Services
{
    public class AlbumService : BaseDataService<ApplicationDbContext>, IAlbumService
    {
        private readonly IMapper _mapper;
        private readonly IAlbumRepository _albumRepository;
        private readonly IJwtHandler _jwtHandler;
        public AlbumService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IAlbumRepository albumRepository,
            IMapper mapper,
            IJwtHandler jwtHandler)
            : base(dbContextWrapper, logger)
        {
            _mapper = mapper;
            _albumRepository = albumRepository;
            _jwtHandler=jwtHandler;
        }
        public async Task<IEnumerable<AlbumDto>> GetAllAlbums()
        {
            return (await ExecuteSafeAsync(() => _albumRepository.GetAlbumsAsync())).Select(x => _mapper.Map<AlbumDto>(x));

        }

        public async Task<IEnumerable<AlbumWithImageDto>> GetUsersAlbumsWithFirstImageAsync(string jwtToken)
        {
            int userId;
            var userPrincipals = _jwtHandler.GetTokenPrincipalsOrNull(jwtToken);
            if (userPrincipals == null)
            {
                throw new BusinessException($"The {jwtToken} is not jwt token");
            }

            var userIdClaim = userPrincipals.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out userId))
            {
                var result = await _albumRepository.GetAlbumsByUserWithImagesAsync(userId, 1);
                return result.Select(x => _mapper.Map<AlbumWithImageDto>(x));
            }
            else
            {
                throw new BusinessException("Token has some problems with id");
            }
        }

        public async Task AddAlbumAsync(AlbumDto addItem)
        {
            await ExecuteSafeAsync(() => _albumRepository.AddAlbumAsync(_mapper.Map<Album>(addItem)));
        }

        public async Task DeleteAlbumAsync(AlbumDto deleteItem)
        {
            await ExecuteSafeAsync(() => _albumRepository.DeleteAlbumAsync(_mapper.Map<Album>(deleteItem)));
        }

        public async Task UpdateAlbumAsync(AlbumDto oldItem, AlbumDto newItem)
        {
            await ExecuteSafeAsync(() => _albumRepository.UpdateAlbumAsync(_mapper.Map<Album>(oldItem), _mapper.Map<Album>(newItem)));
        }

        public async Task<PaginatedItemsResponse<ImageDto>> GetPaginatedImagesAsync(int pageSize, int pageIndex, AlbumDto album)
        {
            return await ExecuteSafeAsync(async () =>
            {
                PaginatedItems<Image> result = await _albumRepository.GetImagesByPageAsync(pageIndex, pageSize, album);
                return new PaginatedItemsResponse<ImageDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(s => _mapper.Map<ImageDto>(s)).ToList(),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            });
        }

        public async Task<PaginatedItemsResponse<AlbumDto>> GetPaginatedAlbumsAsync(int pageSize, int pageIndex)
        {
            return await ExecuteSafeAsync(async () =>
            {
                PaginatedItems<Album> result = await _albumRepository.GetAlbumsByPageAsync(pageIndex, pageSize);
                return new PaginatedItemsResponse<AlbumDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(s => _mapper.Map<AlbumDto>(s)).ToList(),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            });
        }

    }
}
