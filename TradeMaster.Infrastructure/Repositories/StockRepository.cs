using Microsoft.EntityFrameworkCore;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;
using TradeMaster.Core.QueryParameters;
using TradeMaster.Infrastructure.Data;

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

        public async Task<List<Stock>> GetAllStocksAsync(StockQueryParameters query)
        {
            var stocks = _context.Stocks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                stocks = stocks.Where(s =>
                    s.Symbol.Contains(query.Search) ||
                    s.CompanyName.Contains(query.Search));
            }

            if (query.MinPrice.HasValue)
            {
                stocks = stocks.Where(s => s.CurrentPrice >= query.MinPrice.Value);
            }

            if (query.MaxPrice.HasValue)
            {
                stocks = stocks.Where(s => s.CurrentPrice <= query.MaxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "symbol":

                        stocks = query.SortOrder.ToLower() == "desc"
                            ? stocks.OrderByDescending(s => s.Symbol)
                            : stocks.OrderBy(s => s.Symbol);

                        break;

                    case "companyname":

                        stocks = query.SortOrder.ToLower() == "desc"
                            ? stocks.OrderByDescending(s => s.CompanyName)
                            : stocks.OrderBy(s => s.CompanyName);

                        break;

                    case "currentprice":

                        stocks = query.SortOrder.ToLower() == "desc"
                            ? stocks.OrderByDescending(s => s.CurrentPrice)
                            : stocks.OrderBy(s => s.CurrentPrice);

                        break;

                    default:

                        stocks = stocks.OrderBy(s => s.StockID);

                        break;
                }
            }
            else
            {
                stocks = stocks.OrderBy(s => s.StockID);
            }

            return await stocks
             .Skip((query.Page - 1) * query.PageSize)
             .Take(query.PageSize)
             .ToListAsync();
        }

        public async Task<int> GetTotalStockCountAsync()
        {
            return await _context.Stocks.CountAsync();
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
