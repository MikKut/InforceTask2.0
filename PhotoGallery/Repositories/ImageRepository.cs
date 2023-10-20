using Infrastructure;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Api.Host.Data;
using PhotoGallery.Api.Host.Repositories.Interfaces;
using PhotoGallery.Api.Models.DTO;
using PhotoGallery.Api.Models.Entities;

namespace PhotoGallery.Api.Host.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ImageRepository> _logger;

        public ImageRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<ImageRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Image>> GetAlbumsImagesAsync(Album album)
        {
            var relAlbum = await GetRelevantAlbumAsync(album);
            var albumId = relAlbum!.AlbumId;
            return _dbContext.Images.Where(x => x.AlbumId == albumId);
        }

        public async Task LikeImageAsync(Album album, Image image)
        {
            try
            {
                await EnsureAlbumExistsAsync(album);
                image = await GetRelevantImageAsync(image);
                image!.Likes = image!.Likes + 1;
                _ = await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Liked image: {ImageId} in album: {AlbumId}", image.ImageId, album.AlbumId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while liking an image.");
                throw;
            }
        }

        public async Task DislikeImageAsync(Album album, Image image)
        {
            try
            {
                await EnsureAlbumExistsAsync(album);
                image = await GetRelevantImageAsync(image);
                image!.Dislikes = image!.Dislikes + 1;
                _ = await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Disliked image: {ImageId} in album: {AlbumId}", image.ImageId, album.AlbumId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while disliking an image.");
                throw;
            }
        }

        public async Task DeleteImageAsync(Album album, Image imageToDelete)
        {
            try
            {
                await EnsureAlbumExistsAsync(album);
                var theImage = await _dbContext.Images.SingleOrDefaultAsync(x => x.Extension == imageToDelete.Extension && x.Description == imageToDelete.Description);
                if (theImage != null)
                {
                    _ = _dbContext.Images.Remove(theImage);
                    _ = await _dbContext.SaveChangesAsync();
                    _logger.LogInformation("Deleted image: {ImageId} in album: {AlbumId}", theImage.ImageId, album.AlbumId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an image.");
                throw;
            }
        }

        public async Task AddImageAsync(Album album, Image image)
        {
            try
            {
                await EnsureAlbumExistsAsync(album);
                Image? lookedImage = await _dbContext.Images.SingleOrDefaultAsync(x => x.Extension == image.Extension && x.Description == image.Description);
                if (lookedImage != null)
                {
                    _logger.LogWarning("Image already exists: {Extension} - {Description}", image.Extension, image.Description);
                    throw new BusinessException("The image already exists");
                }

                await _dbContext.Images.AddAsync(image);
                _ = await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Added image: {ImageId} to album: {AlbumId}", image.ImageId, album.AlbumId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding an image.");
                throw;
            }
        }

        private async Task<Image> GetRelevantImageAsync(Image image)
        {
            Image? lookedImage = await _dbContext.Images.SingleOrDefaultAsync(x => x.ImageId == image.ImageId);
            if (lookedImage == null)
            {
                _logger.LogWarning("Image not found: {Extension} - {Description}", image.Extension, image.Description);
                throw new BusinessException("The image does not exist");
            }

            return lookedImage;
        }

        private async Task<Album> GetRelevantAlbumAsync(Album album)
        {
            Album? item = await _dbContext.Albums.SingleOrDefaultAsync(x => x.Title == album.Title && x.Description == album.Description);
            if (item == null)
            {
                _logger.LogWarning("Album not found: {Title} - {Description}", album.Title, album.Description);
                throw new BusinessException("The album does not exist");
            }

            return item;
        }

        private async Task EnsureAlbumExistsAsync(Album album)
        {
            Album? item = await _dbContext.Albums.SingleOrDefaultAsync(x => x.AlbumId == album.AlbumId);
            if (item == null)
            {
                _logger.LogWarning("Album not found: {Title} - {Description}", album.Title, album.Description);
                throw new BusinessException("The album does not exist");
            }
        }
    }
}
