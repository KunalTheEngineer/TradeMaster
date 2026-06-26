using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;
using TradeMaster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TradeMaster.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly TradeMasterDbContext _context;

        public StockRepository(TradeMasterDbContext context)
        {
            _context = context;
        }

        public async Task AddStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Stock>> GetAllStocksAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(int stockId)
        {
            return await _context.Stocks.FirstOrDefaultAsync(x => x.StockID == stockId);
        }

        public async Task UpdateStockAsync(Stock stock)
        {
            _context.Stocks.Update(stock);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteStockAsync(Stock stock)
        {
            _context.Stocks.Remove(stock);

            await _context.SaveChangesAsync();
        }
    }
}
