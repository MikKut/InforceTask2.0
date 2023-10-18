using AutoMapper;
using PhotoGallery.Api.Host.Data;
using PhotoGallery.Api.Host.Repositories;
using PhotoGallery.Api.Models.DTO;
using PhotoGallery.Api.Models.Entities;

namespace PhotoGallery.Api.Host.Services
{
    public class ImageService : BaseDataService<ApplicationDbContext>, IImageService
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        public ImageService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IMapper mapper,
            IImageRepository imageRepository)
            : base(dbContextWrapper, logger)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        public async Task<IEnumerable<ImageDto>> GetAlbumsImagesAsync(AlbumDto album)
        {
            return (await ExecuteSafeAsync(() => _imageRepository.GetAlbumsImagesAsync(_mapper.Map<Album>(album)))).Select(x => _mapper.Map<ImageDto>(x));
        }

        public async Task UpdateImageLikeAsync(AlbumDto album, ImageDto imageToUpdateDto, bool isLike)
        {
            if (isLike)
            {
                await ExecuteSafeAsync(() => _imageRepository.LikeImageAsync(_mapper.Map<Album>(album), _mapper.Map<Image>(imageToUpdateDto)));
            }
            else
            {
                await ExecuteSafeAsync(() => _imageRepository.DislikeImageAsync(_mapper.Map<Album>(album), _mapper.Map<Image>(imageToUpdateDto)));
            }
        }

        public async Task DeleteImageAsync(AlbumDto album, ImageDto imageToDeleteDto)
        {
            await ExecuteSafeAsync(() => _imageRepository.DeleteImageAsync(_mapper.Map<Album>(album), _mapper.Map<Image>(imageToDeleteDto)));
        }

        public async Task AddImageAsync(AlbumDto album, ImageDto image)
        {
            await ExecuteSafeAsync(() => _imageRepository.AddImageAsync(_mapper.Map<Album>(album), _mapper.Map<Image>(image)));
        }
    }
}
