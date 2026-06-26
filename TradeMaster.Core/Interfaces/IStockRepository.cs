using TradeMaster.Core.Entities;

namespace TradeMaster.Core.Interfaces
{
    public interface IStockRepository
    {
        Task AddStockAsync(Stock stock);

        Task<List<Stock>> GetAllStocksAsync();  

        Task<Stock?> GetStockByIdAsync(int stockId);

        Task UpdateStockAsync(Stock stock);

        Task DeleteStockAsync(Stock stock);
    }
}
