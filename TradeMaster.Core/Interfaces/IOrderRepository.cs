
using TradeMaster.Core.Entities;

namespace TradeMaster.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);

        Task<List<Order>> GetAllOrdersAsync();

        Task<Order?> GetOrderByIdAsync(int orderId);
    }
}
