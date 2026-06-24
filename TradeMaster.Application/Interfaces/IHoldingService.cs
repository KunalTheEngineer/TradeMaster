
using TradeMaster.Application.DTOs;

namespace TradeMaster.Application.Interfaces
{
    public interface IHoldingService
    {
        Task<List<HoldingResponseDto>> GetAllHoldingsAsync();

        Task<List<HoldingResponseDto>> GetHoldingsByUserIdAsync(int userId);
    }
}
