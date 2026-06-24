using TradeMaster.Application.DTOs;

namespace TradeMaster.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequestDto request);

        Task<string> LoginAsync(LoginRequestDto request);
    }
}
