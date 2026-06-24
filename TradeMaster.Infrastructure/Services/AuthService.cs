using TradeMaster.Application.DTOs;
using TradeMaster.Application.Interfaces;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;

namespace TradeMaster.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
                PasswordHash = request.Password
            };

            await _userRepository.AddUserAsync(user);

            return "User Registered Successfully !";
        }

        public async Task<string> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if(user ==  null)
            {
                return "User Not Found !";
            }

            if(user.PasswordHash != request.Password)
            {
                return "Invalid Password !";
            }

            return "Login Successful !";
        }
    }
}
