using AutoMapper;
using Microsoft.Extensions.Logging;
using PhotoGallery.Api.Host.Data.Authorization;
using PhotoGallery.Api.Host.Repositories;
using PhotoGallery.Api.Models.Entities;
using PhotoGallery.Api.Models.Requests;
using PhotoGallery.Api.Models.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PhotoGallery.Api.Host.Services
{
    public class UserService : IUserService
    {
        private readonly IJwtHandler _jwtHandler;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IJwtHandler jwtHandler, IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _userRepository = userRepository;
        }

        public List<string>? GetUserRolesByTokenOrNull(string jwtToken)
        {
            var prinicpals = _jwtHandler.GetTokenPrincipalsOrNull(jwtToken);
            if (prinicpals is null)
            {
                return null;
            }

            return prinicpals.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetUserAsync(request.Email, request.Password);
            if (user == null)
            {
                _logger.LogInformation($"User was not found");
                return new LoginResponse()
                {
                    Token = null,
                    Success = false,
                    Message = "Invalid Email or Password."
                };
            }

            _logger.LogInformation($"User was found: {user.UserName}, {user.Email}");
            var secToken = await _jwtHandler.GetTokenAsync(user);
            var jwt = new JwtSecurityTokenHandler().WriteToken(secToken);
            _logger.LogInformation(jwt);
            return new LoginResponse()
            {
                Success = true,
                Message = "Login successful",
                Token = jwt
            };
        }
    }
}
