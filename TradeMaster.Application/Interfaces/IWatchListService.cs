using TradeMaster.Application.DTOs;

namespace TradeMaster.Application.Interfaces
{
    public interface IWatchListService
    {
        Task<string> AddToWatchlistAsync(AddWatchListDto dto);

        Task<List<WatchListDto>> GetUserWatchlistAsync(int userId);

        Task<string> RemoveFromWatchlistAsync(int userId, int stockId);
    }
}
