using TradeMaster.Application.DTOs;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;
using TradeMaster.Application.Interfaces;

namespace TradeMaster.Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<string> AddStockAsync(CreateStockDto request)
        {
            var stock = new Stock
            {
                Symbol = request.Symbol,
                CompanyName = request.CompanyName,
                CurrentPrice = request.CurrentPrice
            };

            await _stockRepository.AddStockAsync(stock);

            return "Stock Added Successfully";
        }

        public async Task<List<StockResponseDto>> GetAllStockAsync()
        {
            var stocks = await _stockRepository.GetAllStocksAsync();

            return stocks.Select(x => new StockResponseDto 
            { StockId = x.StockID, 
              Symbol = x.Symbol, 
              CompanyName = x.CompanyName,
              CurrentPrice = x.CurrentPrice }).ToList();
        }

        public async Task<StockResponseDto?> GetStockByIdAsync(int stockId)
        {
            var stock = await _stockRepository.GetStockByIdAsync(stockId);

            if(stock == null)
            {
                return null;
            }

            return new StockResponseDto
            {
                StockId = stock.StockID,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                CurrentPrice = stock.CurrentPrice
            };
        }

        public async Task<string> UpdateStockAsync(int stockId, UpdateStockDto request)
        {
            var stock = await _stockRepository.GetStockByIdAsync(stockId);

            if (stock == null)
            {
                return "Stock Not Found !";
            }
            
            stock.Symbol = request.Symbol;
            stock.CompanyName = request.CompnayName;
            stock.CurrentPrice = request.CurrentPrice;

            await _stockRepository.UpdateStockAsync(stock);

            return "Stock Updated Successfully !";
            
        }

        public async Task<string> DeleteStockAsync(int stockId)
        {
            var stock = await _stockRepository.GetStockByIdAsync(stockId);

            if(stock == null)
            {
                return "Stock Not Found !";
            }

            await _stockRepository.DeleteStockAsync(stock);

            return "Stock Deleted Successfully !";
        }
    }
}
