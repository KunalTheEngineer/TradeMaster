using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Core.Entities;

namespace TradeMaster.Core.Interfaces
{
    public interface IWatchListRepository
    {
        Task AddToWatchlistAsync(Watchlist watchlist);

        Task<List<Watchlist>> GetUserWatchlistAsync(int userId);

        Task<Watchlist?> GetWatchlistItemAsync(int userId, int stockId);

        Task RemoveFromWatchlistAsync(Watchlist watchlist);
    }
}
