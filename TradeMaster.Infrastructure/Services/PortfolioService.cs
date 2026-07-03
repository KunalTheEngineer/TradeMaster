using Microsoft.Extensions.Logging;
using TradeMaster.Application.DTOs;
using TradeMaster.Application.Interfaces;
using TradeMaster.Core.Interfaces;

namespace TradeMaster.Infrastructure.Services
{
    public class PortfolioService : IPortfolioService
    {
        //HOLDINGS table contain quntity and avgprice so we taken this below
        private readonly IHoldingRepository _holdingRepository;
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<PortfolioService> _logger;
        public PortfolioService(IHoldingRepository holdingRepository, IStockRepository stockRepository, ILogger<PortfolioService> logger)
        {
            _holdingRepository = holdingRepository;
            _stockRepository = stockRepository;
            _logger = logger;
        }

        public async Task<PortfolioSummaryDto> GetPortfolioSummaryAsync(int userId)
        {
            _logger.LogInformation(
            "PORTFOLIO | User: {UserId} requested portfolio summary.",
            userId);

            var holdings = await _holdingRepository.GetHoldingsByUserIdAsync(userId);

            if (!holdings.Any())
            {
                _logger.LogWarning(
                    "PORTFOLIO | User: {UserId} has no holdings.",
                    userId);
            }

            return new PortfolioSummaryDto
            {
                TotalHoldings = holdings.Count,
                TotalQuantity = holdings.Sum(h => h.Qunatity),
                TotalInvestment = holdings.Sum(h => h.Qunatity * h.AveragePrice)
            };
        }

        public async Task<List<ProfitLossDto>> GetProfitLossAsync(int userId)
        {
            _logger.LogInformation(
            "PORTFOLIO | User: {UserId} requested profit/loss report.",
            userId);

            var holdings = await _holdingRepository.GetHoldingsByUserIdAsync(userId);

            if (!holdings.Any())
            {
                _logger.LogWarning(
                    "PORTFOLIO | User: {UserId} has no holdings for profit/loss calculation.",
                    userId);
            }

            var profitLossList = new List<ProfitLossDto>();

            foreach (var holding in holdings)
            {
                var stock = await _stockRepository.GetStockByIdAsync(holding.StockID);

                if (stock == null)
                    continue;

                decimal totalInvestment = holding.Qunatity * holding.AveragePrice;

                decimal currentValue = holding.Qunatity * stock.CurrentPrice;

                decimal profitLoss = currentValue - totalInvestment;

                profitLossList.Add(new ProfitLossDto
                {
                    StockName = stock.CompanyName,
                    Quantity = holding.Qunatity,
                    AveragePrice = holding.AveragePrice,
                    CurrentPrice = stock.CurrentPrice,
                    TotalInvestment = totalInvestment,
                    CurrentValue = currentValue,
                    ProfitLoss = profitLoss
                });
            }

            return profitLossList;
        }
    }
}
