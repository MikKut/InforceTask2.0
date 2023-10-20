using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Api.Host.Services.Interfaces;
using PhotoGallery.Api.Models.Entities;
using PhotoGallery.Api.Models.Requests;
using PhotoGallery.Api.Models.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace PhotoGallery.Api.Host.Controllers
{
    [AllowAnonymous]
    [Route(ComponentDefaults.DefaultApiRoute)]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
            {
            var response = await _userService.LoginAsync(loginRequest);
            if (!response.Success)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }
    }
}
