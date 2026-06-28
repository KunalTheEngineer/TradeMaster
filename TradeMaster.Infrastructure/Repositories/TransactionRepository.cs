
using Microsoft.EntityFrameworkCore;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;
using TradeMaster.Infrastructure.Data;

namespace TradeMaster.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TradeMasterDbContext _context;

        public TransactionRepository(TradeMasterDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetAllTransactionAsync()
        {
            return await _context.Transactions.ToListAsync();
        }
    }
}
