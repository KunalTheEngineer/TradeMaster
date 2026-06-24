
using TradeMaster.Application.DTOs;
using TradeMaster.Application.Interfaces;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;

namespace TradeMaster.Infrastructure.Services
{
    public class HoldingService : IHoldingService
    {
        private readonly IHoldingRepository _holdingRepository;

        public HoldingService(IHoldingRepository holdingRepository)
        {
            _holdingRepository = holdingRepository;
        }

        public async Task<List<HoldingResponseDto>> GetAllHoldingsAsync()
        {
            var holdings = await _holdingRepository.GetAllHoldingsAsync();

            return holdings.Select(x => new HoldingResponseDto
            {
                HoldingId = x.HoldingID,
                UserId = x.UserID,
                StockId = x.StockID,
                Quantity = x.Qunatity,
                AveragePrice = x.AveragePrice
            }).ToList();
        }

        public async Task<List<HoldingResponseDto>> GetHoldingsByUserIdAsync(int userId)
        {
            var holdings = await _holdingRepository.GetHoldingsByUserIdAsync(userId);

            return holdings.Select(x => new HoldingResponseDto
            {
                HoldingId = x.HoldingID,
                UserId = x.UserID,
                StockId = x.StockID,
                Quantity = x.Qunatity,
                AveragePrice = x.AveragePrice
            }).ToList();
        }

    }
}
