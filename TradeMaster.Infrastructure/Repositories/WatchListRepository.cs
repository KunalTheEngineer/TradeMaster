using Microsoft.EntityFrameworkCore;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;
using TradeMaster.Infrastructure.Data;

namespace TradeMaster.Infrastructure.Repositories
{
    public class WatchListRepository :IWatchListRepository
    {
        private readonly TradeMasterDbContext _context;

        public WatchListRepository(TradeMasterDbContext context)
        {
            _context = context;
        }

        public async Task AddToWatchlistAsync(Watchlist watchlist)
        {
            await _context.Watchlists.AddAsync(watchlist);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Watchlist>> GetUserWatchlistAsync(int userId)
        {
            return await _context.Watchlists
                .Where(w => w.UserId == userId)
                .Include(w => w.Stock)
                .ToListAsync();
        }

        public async Task<Watchlist?> GetWatchlistItemAsync(int userId, int stockId)
        {
            return await _context.Watchlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.StockId == stockId);
        }

        public async Task RemoveFromWatchlistAsync(Watchlist watchlist)
        {
            _context.Watchlists.Remove(watchlist);
            await _context.SaveChangesAsync();
        }
    }
}
