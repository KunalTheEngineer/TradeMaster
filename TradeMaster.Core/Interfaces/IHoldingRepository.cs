
using TradeMaster.Core.Entities;

namespace TradeMaster.Core.Interfaces
{
    public interface IHoldingRepository
    {
        Task<List<Holding>> GetAllHoldingsAsync();

        Task<List<Holding>> GetHoldingsByUserIdAsync(int UserId);
    }
}
