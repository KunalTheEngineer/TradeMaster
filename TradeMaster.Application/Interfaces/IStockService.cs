
using TradeMaster.Application.DTOs;
using TradeMaster.Core.QueryParameters;

namespace TradeMaster.Application.Interfaces
{
    public interface IStockService
    {
        Task<string> AddStockAsync(CreateStockDto request);

        Task<PagedResponse<StockResponseDto>> GetAllStockAsync(StockQueryParameters query);

        Task<StockResponseDto?> GetStockByIdAsync(int stockId);

        Task<string> UpdateStockAsync(int stockId, UpdateStockDto request);

        Task<string> DeleteStockAsync(int stockId);

        
    }
}
