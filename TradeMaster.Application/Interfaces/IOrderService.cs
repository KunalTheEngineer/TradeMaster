
using TradeMaster.Application.DTOs;

namespace TradeMaster.Application.Interfaces
{
    public interface IOrderService
    {
        Task<string> BuyOrderAsync(CreateOrderDto request);

        Task<List<OrderResponseDto>> GetAllOrdersAsync();

        Task<OrderResponseDto?> GetOrderByIdAsync(int orderId);

        Task<string> SellOrderAsync(CreateOrderDto request);
    }
}
