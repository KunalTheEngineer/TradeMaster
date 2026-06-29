using TradeMaster.Application.DTOs;
using System.Collections.Generic;

namespace TradeMaster.Application.Interfaces
{
    public interface IPortfolioService
    {
        Task<PortfolioSummaryDto> GetPortfolioSummaryAsync(int userId);

        Task<List<ProfitLossDto>> GetProfitLossAsync(int userId);
    }
}
