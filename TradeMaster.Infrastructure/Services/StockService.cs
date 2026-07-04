using TradeMaster.Application.DTOs;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;
using TradeMaster.Application.Interfaces;
using Microsoft.Extensions.Logging;
using TradeMaster.Core.QueryParameters;

namespace TradeMaster.Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<StockService> _logger;
        public StockService(IStockRepository stockRepository, ILogger<StockService> logger)
        {
            _stockRepository = stockRepository;
            _logger = logger;
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

            _logger.LogInformation(
              "ADMIN ACTION | Added Stock | Symbol: {Symbol} | Company: {CompanyName} | Price: {Price}",
             stock.Symbol,
             stock.CompanyName,
             stock.CurrentPrice);

            return "Stock Added Successfully";
        }

        public async Task<PagedResponse<StockResponseDto>> GetAllStockAsync(StockQueryParameters query)
        {
            var stocks = await _stockRepository.GetAllStocksAsync(query);

            var totalRecords = await _stockRepository.GetTotalStockCountAsync();

            return new PagedResponse<StockResponseDto>
            {
                Data = stocks.Select(x => new StockResponseDto
                {
                    StockId = x.StockID,
                    Symbol = x.Symbol,
                    CompanyName = x.CompanyName,
                    CurrentPrice = x.CurrentPrice
                }).ToList(),

                Page = query.Page,

                PageSize = query.PageSize,

                TotalRecords = totalRecords,

                TotalPages = (int)Math.Ceiling((double)totalRecords / query.PageSize)
            };
        }

        public async Task<StockResponseDto?> GetStockByIdAsync(int stockId)
        {
            var stock = await _stockRepository.GetStockByIdAsync(stockId);

            if(stock == null)
            {
                _logger.LogWarning(
                 "ADMIN ACTION | Update failed. Stock {StockId} not found.",
                     stockId);

                throw new KeyNotFoundException("Stock not found.");
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
                if (stock == null)
                {
                    _logger.LogWarning(
                        "ADMIN ACTION | Update failed. Stock {StockId} not found.",
                        stockId);

                    throw new KeyNotFoundException("Stock not found.");
                }
            }
            
            stock.Symbol = request.Symbol;
            stock.CompanyName = request.CompnayName;
            stock.CurrentPrice = request.CurrentPrice;

            await _stockRepository.UpdateStockAsync(stock);

            _logger.LogInformation(
             "ADMIN ACTION | Updated Stock | Symbol: {Symbol} | Company: {CompanyName} | New Price: {Price}",
             stock.Symbol,
             stock.CompanyName,
             stock.CurrentPrice);

            return "Stock Updated Successfully !";
            
        }

        public async Task<string> DeleteStockAsync(int stockId)
        {
            var stock = await _stockRepository.GetStockByIdAsync(stockId);

            if(stock == null)
            {
                _logger.LogWarning(
                  "ADMIN ACTION | Delete failed. Stock {StockId} not found.",
                    stockId);

                throw new KeyNotFoundException("Stock not found.");
            }

            await _stockRepository.DeleteStockAsync(stock);
            _logger.LogInformation(
              "ADMIN ACTION | Deleted Stock | Symbol: {Symbol}",
             stock.Symbol);

            return "Stock Deleted Successfully !";
        }
    }
}
