using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Api.Host.Data.Authorization;
using PhotoGallery.Api.Host.Services.Interfaces;
using PhotoGallery.Api.Models.DTO;
using PhotoGallery.Api.Models.Entities;
using PhotoGallery.Api.Models.Requests;
using PhotoGallery.Api.Models.Responses;
using System.Net;
using Roles = Infrastructure.Roles;

namespace PhotoGallery.Api.Host.Controllers
{

    [Route(ComponentDefaults.DefaultApiRoute)]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService albumService)
        {
            _imageService = albumService;
        }

        [HttpPost]
        [Authorize(Policy = AuthPolicy.UserPolicy)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddImage(ImageRequest request)
        {
            try
            {
                await _imageService.AddImageAsync(request.Album, request.Image);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = AuthPolicy.UserPolicy)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LikeImage(LikeImageRequest request)
        {
            try
            {
                await _imageService.UpdateImageLikeAsync(
                    new AlbumDto()
                    {
                        Id = request.AlbumId,
                        Description = request.AlbumDescription,
                        Title = request.AlbumTitle
                    },
                    new ImageDto()
                    {
                        Id = request.ImageId,
                        Description= request.ImageDescription,
                        Title = request.ImageTitle,
                        Extension = request.ImageExtension
                    },
                    true);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = AuthPolicy.UserPolicy)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DislikeImage(LikeImageRequest request)
        {
            try
            {
                await _imageService.UpdateImageLikeAsync(
                    new AlbumDto()
                    {
                        Id = request.AlbumId,
                        Description = request.AlbumDescription,
                        Title = request.AlbumTitle
                    },
                    new ImageDto()
                    {
                        Id = request.ImageId,
                        Description= request.ImageDescription,
                        Title = request.ImageTitle,
                        Extension = request.ImageExtension
                    },
                    false);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Authorize(Policy = AuthPolicy.UserPolicy)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteImage(ImageRequest request)
        {
            try
            {
                await _imageService.DeleteImageAsync(request.Album, request.Image);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = AuthPolicy.UserPolicy)]
        [ProducesResponseType(typeof(CollectionResponse<ImageDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAlbumsImage(AlbumDto request)
        {
            try
            {
                var images = await _imageService.GetAlbumsImagesAsync(request);
                return Ok(images);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
