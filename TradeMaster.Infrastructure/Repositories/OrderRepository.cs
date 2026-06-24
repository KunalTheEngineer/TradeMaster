
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;
using TradeMaster.Infrastructure.Data;

namespace TradeMaster.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TradeMasterDbContext _context;

        public OrderRepository(TradeMasterDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
        }
    }
}
