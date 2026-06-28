using TradeMaster.Application.DTOs;
using TradeMaster.Application.Interfaces;
using TradeMaster.Core.Interfaces;

namespace TradeMaster.Infrastructure.Services
{
    public class PortfolioService : IPortfolioService
    {
        //HOLDINGS table contain quntity and avgprice so we taken this below
        private readonly IHoldingRepository _holdingRepository;

        public PortfolioService(IHoldingRepository holdingRepository)
        {
            _holdingRepository = holdingRepository;
        }

        public async Task<PortfolioSummaryDto> GetPortfolioSummaryAsync(int userId)
        {
            var holdings = await _holdingRepository.GetHoldingsByUserIdAsync(userId);

            return new PortfolioSummaryDto
            {
                TotalHoldings = holdings.Count,
                TotalQuantity = holdings.Sum(h => h.Qunatity),
                TotalInvestment = holdings.Sum(h => h.Qunatity * h.AveragePrice)
            };
        }
    }
}
