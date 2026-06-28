

using TradeMaster.Core.Entities;

namespace TradeMaster.Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddTransactionAsync(Transaction transaction);

        Task<List<Transaction>> GetAllTransactionAsync();
    }
}
