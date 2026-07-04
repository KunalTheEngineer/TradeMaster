using TradeMaster.Core.Entities;
using TradeMaster.Core.QueryParameters;

namespace TradeMaster.Core.Interfaces
{
    public interface IStockRepository
    {
        Task AddStockAsync(Stock stock);

        Task<List<Stock>> GetAllStocksAsync(StockQueryParameters query);

        Task<int> GetTotalStockCountAsync();

        Task<Stock?> GetStockByIdAsync(int stockId);

        Task UpdateStockAsync(Stock stock);

        Task DeleteStockAsync(Stock stock);
    }
}
