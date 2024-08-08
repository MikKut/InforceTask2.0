using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Api.Host.Data.Authorization;
using PhotoGallery.Api.Host.Services.Interfaces;
using PhotoGallery.Api.Models.DTO;
using PhotoGallery.Api.Models.Requests;
using PhotoGallery.Api.Models.Responses;
using System.Net;

namespace PhotoGallery.Api.Host.Controllers
{
    [Route(ComponentDefaults.DefaultApiRoute)]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpPost]
        [Authorize(Policy = AuthPolicy.UserPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAlbum(AddItemRequest<AlbumDto> albumDto)
        {
            try
            {
                await _albumService.AddAlbumAsync(albumDto.Item);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Policy = AuthPolicy.AdminPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(DeleteRequest<AlbumDto> request)
        {
            try
            {
                await _albumService.DeleteAlbumAsync(request.Item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = AuthPolicy.AdminPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateAllRequest<AlbumDto> request)
        {
            try
            {
                await _albumService.UpdateAlbumAsync(request.OldItem, request.NewItem);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = AuthPolicy.UserPolicy)]
        [ProducesResponseType(typeof(PaginatedItemsResponse<ImageDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ImagesByAlbum(PaginatedImageRequest request)
        {
            try
            {
                var images = await _albumService.GetPaginatedImagesAsync(request.PageSize, request.PageIndex, request.AlbumId);
                return Ok(images);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PaginatedItemsResponse<AlbumWithImageDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Albums(PaginatedAlbumRequest request)
        {
            try
            {
                var albums = await _albumService.GetPaginatedAlbumsAsync(request.PageSize, request.PageIndex);
                return Ok(albums);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = AuthPolicy.UserPolicy)]
        [ProducesResponseType(typeof(IEnumerable<AlbumWithImageDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsersAlbumsWithFirstImage()
        {
            try
            {
                var albums = await _albumService.GetUsersAlbumsWithFirstImageAsync(HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
                return Ok(albums);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
