
using TradeMaster.Application.DTOs;
using TradeMaster.Application.Interfaces;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Enums;
using TradeMaster.Core.Interfaces;

namespace TradeMaster.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<string> BuyOrderAsync(CreateOrderDto request)
        {
            var order = new Order
            {
                UserId = request.UserId,
                StockId = request.StockId,
                Quantity = request.Quantity,
                Price = request.Price,
                OrderType = OrderType.Buy,   
                OrderStatus = OrderStatus.Completed,
                OrderDate = DateTime.UtcNow,
            };

            await _orderRepository.AddOrderAsync(order);

            return "Buy Order Placed Successfully !";
        }

        public async Task<List<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            return orders.Select(x => new OrderResponseDto
            {
                OrderId = x.OrderId,
                UserId = x.UserId,
                StockId = x.StockId,
                Quantity = x.Quantity,
                Price = x.Price,
                OrderDate = x.OrderDate,
                OrderType = x.OrderType.ToString(),
                OrderStatus = x.OrderStatus.ToString()
            }).ToList();
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            if(order == null)
            {
                return null;
            }

            return new OrderResponseDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                StockId = order.StockId,
                Quantity = order.Quantity,
                Price = order.Price,
                OrderDate = order.OrderDate,
                OrderType = order.OrderType.ToString(),
                OrderStatus = order.OrderStatus.ToString()
            };
        }
    }
}
