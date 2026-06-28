using TradeMaster.Application.DTOs;

namespace TradeMaster.Application.Interfaces
{
    public interface IPortfolioService
    {
        Task<PortfolioSummaryDto> GetPortfolioSummaryAsync(int userId);
    }
}
