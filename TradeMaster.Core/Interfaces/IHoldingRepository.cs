
using TradeMaster.Core.Entities;

namespace TradeMaster.Core.Interfaces
{
    public interface IHoldingRepository
    {
        Task<List<Holding>> GetAllHoldingsAsync();

        Task<List<Holding>> GetHoldingsByUserIdAsync(int userId);

        Task<Holding?> GetHoldingByUserAndStockAsync(int userId, int stockId);

        Task AddHoldingAsync(Holding holding);
        
        Task UpdateHoldingAsync(Holding holding);
    }
}
