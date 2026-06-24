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
    }
}
