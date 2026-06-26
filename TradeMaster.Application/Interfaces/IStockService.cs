
using TradeMaster.Application.DTOs;

namespace TradeMaster.Application.Interfaces
{
    public interface IStockService
    {
        Task<string> AddStockAsync(CreateStockDto request);

        Task<List<StockResponseDto>> GetAllStockAsync();

        Task<StockResponseDto?> GetStockByIdAsync(int stockId);

        Task<string> UpdateStockAsync(int stockId, UpdateStockDto request);

        Task<string> DeleteStockAsync(int stockId);
    }
}
