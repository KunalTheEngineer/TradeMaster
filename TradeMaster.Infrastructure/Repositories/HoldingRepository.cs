using Microsoft.EntityFrameworkCore;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;
using TradeMaster.Infrastructure.Data;

namespace TradeMaster.Infrastructure.Repositories
{
    public class HoldingRepository : IHoldingRepository
    {
        private readonly TradeMasterDbContext _context;

        public HoldingRepository(TradeMasterDbContext context)
        {
            _context = context;
        }

        public async Task<List<Holding>> GetAllHoldingsAsync()
        {
            return await _context.Holdings.ToListAsync();   
        }

        public async Task<List<Holding>> GetHoldingsByUserIdAsync(int userId)
        {

            return await _context.Holdings.Where(x => x.UserID == userId).ToListAsync();
        }

        public async Task<Holding?> GetHoldingByUserAndStockAsync(int userId, int stockId)
        {
            return await _context.Holdings.FirstOrDefaultAsync(x => x.UserID == userId && 
                                                                    x.StockID == stockId);
        }

        public async Task AddHoldingAsync(Holding holding)
        {
            await _context.Holdings.AddAsync(holding);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateHoldingAsync(Holding holding)
        {
            _context.Holdings.Update(holding);

            await _context.SaveChangesAsync();
        }
    }
}
