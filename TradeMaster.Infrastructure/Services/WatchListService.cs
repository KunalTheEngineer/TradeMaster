using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Application.DTOs;
using TradeMaster.Application.Interfaces;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;
using TradeMaster.Infrastructure.Repositories;

namespace TradeMaster.Infrastructure.Services
{
    public class WatchListService : IWatchListService
    {
        private readonly IWatchListRepository _watchlistRepository;
        private readonly ILogger<WatchListService> _logger;
        private readonly IStockRepository _stockRepository;

        public WatchListService(IWatchListRepository repository, IStockRepository stockRepository, ILogger<WatchListService> logger)
        {
            _watchlistRepository = repository;
            _stockRepository = stockRepository;
            _logger = logger;
        }

        public async Task<string> AddToWatchlistAsync(AddWatchListDto dto)
        {
            var stock = await _stockRepository.GetStockByIdAsync(dto.StockId);

            if (stock == null)
            {
                _logger.LogWarning(
                    "WATCHLIST | User: {UserId} | Stock {StockId} not found.",
                    dto.UserId,
                    dto.StockId);

                throw new KeyNotFoundException("Stock not found.");
            }

            var existing = await _watchlistRepository
                .GetWatchlistItemAsync(dto.UserId, dto.StockId);

            if (existing != null)
            {
                _logger.LogWarning(
                "WATCHLIST | User: {UserId} | Stock: {Symbol} already exists in watchlist.",
                dto.UserId,
                 stock.Symbol);

                throw new ArgumentException("Stock already exists in watchlist.");
            }
               
            var watchlist = new Watchlist
            {
                UserId = dto.UserId,
                StockId = dto.StockId
            };

            await _watchlistRepository.AddToWatchlistAsync(watchlist);
            
            _logger.LogInformation(
                "WATCHLIST | User: {UserId} | Added Stock: {Symbol}",
                dto.UserId,
                stock.Symbol);

            return "Stock added to watchlist successfully.";
        }

        public async Task<List<WatchListDto>> GetUserWatchlistAsync(int userId)
        {
            var watchlist = await _watchlistRepository
                .GetUserWatchlistAsync(userId);

            return watchlist.Select(w => new WatchListDto
            {
                WatchlistId = w.WatchlistId,
                Symbol = w.Stock.Symbol,
                CompanyName = w.Stock.CompanyName,
                CurrentPrice = w.Stock.CurrentPrice
            }).ToList();
        }

        public async Task<string> RemoveFromWatchlistAsync(int userId, int stockId)
        {
            var stock = await _stockRepository.GetStockByIdAsync(stockId);

            if (stock == null)
            {
                _logger.LogWarning(
                    "WATCHLIST | User: {UserId} | Stock {StockId} not found.",
                    userId,
                    stockId);

                throw new KeyNotFoundException("Stock not found.");
            }

            var watchlist = await _watchlistRepository
                .GetWatchlistItemAsync(userId, stockId);

            if (watchlist == null)
            {
                _logger.LogWarning(
                  "WATCHLIST | User: {UserId} | Stock: {Symbol} not found in watchlist.",
                   userId,
                    stock.Symbol);

                throw new KeyNotFoundException("Stock not found in watchlist.");
            }
            
            await _watchlistRepository.RemoveFromWatchlistAsync(watchlist);
            _logger.LogInformation(
            "WATCHLIST | User: {UserId} | Removed Stock: {Symbol}",
            userId,
            stock.Symbol);

            return "Stock removed from watchlist successfully.";
        }
    }
}
