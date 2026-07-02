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

        public WatchListService(IWatchListRepository repository)
        {
            _watchlistRepository = repository;
        }

        public async Task<string> AddToWatchlistAsync(AddWatchListDto dto)
        {
            var existing = await _watchlistRepository
                .GetWatchlistItemAsync(dto.UserId, dto.StockId);

            if (existing != null)
                return "Stock already exists in watchlist.";

            var watchlist = new Watchlist
            {
                UserId = dto.UserId,
                StockId = dto.StockId
            };

            await _watchlistRepository.AddToWatchlistAsync(watchlist);

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
            var watchlist = await _watchlistRepository
                .GetWatchlistItemAsync(userId, stockId);

            if (watchlist == null)
                return "Stock not found in watchlist.";

            await _watchlistRepository.RemoveFromWatchlistAsync(watchlist);

            return "Stock removed from watchlist successfully.";
        }
    }
}
