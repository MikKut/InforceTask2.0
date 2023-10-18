using Catalog.Host.Data;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Api.Host.Data;
using PhotoGallery.Api.Models.DTO;
using PhotoGallery.Api.Models.Entities;
using PhotoGallery.Api.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace PhotoGallery.Api.Host.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<AlbumRepository> _logger;

        public AlbumRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<AlbumRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Album>> GetAlbumsAsync()
        {
            try
            {
                var searchedAlbums = await _dbContext.Albums.ToListAsync();
                if (!searchedAlbums.Any())
                {
                    throw new BusinessException("There is no any alnums");
                }

                return searchedAlbums;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting albums.");
                throw;
            }
        }

        public async Task<IEnumerable<Album>> GetAlbumsByUserWithImagesAsync(int userId, int quantityOfImages)
        {
            try 
            {
                if (userId <= 0 || quantityOfImages < 0)
                {
                    throw new BusinessException($"UserId: {userId} or qantity if images: {quantityOfImages} for album to take is wrong");
                }

                var albums = await _dbContext.Albums.Where(x => x.UserId == userId).Include(x => x.Images.Take(quantityOfImages)).ToListAsync();
                return albums;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred while getting albums with images.");
                throw;
            }
        }

        public async Task<PaginatedItems<Album>> GetAlbumsByPageAsync(int pageIndex, int pageSize)
        {
            return new PaginatedItems<Album>()
            {
                Data = await _dbContext.Albums.Skip(pageSize * pageIndex)
                    .Take(pageSize).ToListAsync(),
                TotalCount = await _dbContext.Albums.LongCountAsync()
            };
        }

        public async Task<PaginatedItems<Image>> GetImagesByPageAsync(int pageIndex, int pageSize, AlbumDto album)
        {
            try
            {
                var albumId = await GetAlbumIdAsync(album);

                long totalItems = await _dbContext.Images
                    .Where(x => x.AlbumId == albumId)
                    .LongCountAsync();
                List<Image> itemsOnPage = await _dbContext.Images
                    .Include(i => i.Album)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize)
                    .ToListAsync();

                _logger.LogInformation("Retrieved images by page:");
                foreach (Image? item in itemsOnPage)
                {
                    _logger.LogInformation($"{item.ImageId} - {item.Title}");
                }

                _logger.LogInformation($"Ended with {totalItems} total items and {itemsOnPage.Count} items on page");
                return new PaginatedItems<Image>() { TotalCount = totalItems, Data = itemsOnPage };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching images by page.");
                throw;
            }
        }

        public async Task<int> GetAlbumIdAsync(AlbumDto album)
        {
            try
            {
                var searchedAlbum = await _dbContext.Albums.SingleOrDefaultAsync(x => x.Title == album.Title && x.Description == album.Description);
                if (searchedAlbum == null)
                {
                    _logger.LogWarning("Album not found: {Title} - {Description}", album.Title, album.Description);
                    throw new BusinessException("There is no such album");
                }

                return searchedAlbum.AlbumId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the album ID.");
                throw;
            }
        }

        public async Task AddAlbumAsync(Album addItem)
        {
            try
            {
                if (await CheckIfAlbumExists(addItem))
                {
                    _logger.LogWarning("Album already exists: {Title} - {Description}", addItem.Title, addItem.Description);
                    throw new BusinessException("The album already exists");
                }

                await _dbContext.Albums.AddAsync(addItem);
                _ = await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Album added: {Title} - {Description}", addItem.Title, addItem.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding an album.");
                throw;
            }
        }

        public async Task DeleteAlbumAsync(Album deleteItem)
        {
            try
            {
                if (!await CheckIfAlbumExists(deleteItem))
                {
                    _logger.LogWarning("Album not found for deletion: {Title} - {Description}", deleteItem.Title, deleteItem.Description);
                    throw new BusinessException("The album does not exist");
                }

                _dbContext.Albums.Remove(deleteItem);
                _ = await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Album deleted: {Title} - {Description}", deleteItem.Title, deleteItem.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an album.");
                throw;
            }
        }

        public async Task<bool> UpdateAlbumAsync(Album oldAlbum, Album newAlbum)
        {
            try
            {
                Album? item = await _dbContext.Albums.SingleOrDefaultAsync(x => x.Title == oldAlbum.Title && x.Description == oldAlbum.Description);
                if (item == null)
                {
                    _logger.LogWarning("Album not found for update: {Title} - {Description}", oldAlbum.Title, oldAlbum.Description);
                    throw new BusinessException("The album does not exist");
                }

                item!.Title = newAlbum.Title!;
                item!.Description = newAlbum.Description!;
                item!.Images = newAlbum.Images;
                _ = await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Album updated: {Title} - {Description}", newAlbum.Title, newAlbum.Description);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating an album.");
                throw;
            }
        }

        private async Task<bool> CheckIfAlbumExists(Album album)
        {
            var searchedAlbum = await _dbContext.Albums.SingleOrDefaultAsync(x => x.Title == album.Title && x.Description == album.Description);
            if (searchedAlbum == null)
            {
                return false;
            }

            return true;
        }
    }
}
