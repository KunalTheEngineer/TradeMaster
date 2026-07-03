using TradeMaster.Application.DTOs;
using TradeMaster.Application.Interfaces;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace TradeMaster.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, IJwtService jwtService, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<string> RegisterAsync(RegisterRequestDto request)
        {
            var exisitingUser = await _userRepository.GetByEmailAsync(request.Email);

            if(exisitingUser != null)
            {
                return "Email already exists !";
            }

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.Now,
                Role = "User"
            };

            await _userRepository.AddUserAsync(user);

            return "User Registered Successfully !";
        }

        public async Task<string> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                _logger.LogWarning(
                    "Login failed. User with email {Email} not found.",
                    request.Email);

                return "User Not Found !";
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isValidPassword)
            {
                _logger.LogWarning(
                    "Login failed. Invalid password for email {Email}.",
                    request.Email);

                return "Invalid Password !";
            }

            var token = _jwtService.GenerateToken(user);

            _logger.LogInformation(
                "User {UserId} ({Email}) logged in successfully.",
                user.USerID,
                user.Email);

            return token;
        }
    }
}
