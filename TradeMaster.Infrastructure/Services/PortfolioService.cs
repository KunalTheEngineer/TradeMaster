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

        public PortfolioService(IHoldingRepository holdingRepository, IStockRepository stockRepository)
        {
            _holdingRepository = holdingRepository;
            _stockRepository = stockRepository;
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

        public async Task<List<ProfitLossDto>> GetProfitLossAsync(int userId)
        {
            var holdings = await _holdingRepository.GetHoldingsByUserIdAsync(userId);

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
