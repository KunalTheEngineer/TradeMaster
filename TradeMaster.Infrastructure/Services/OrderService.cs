
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
        private readonly IHoldingRepository _holdingRepository;
        private readonly ITransactionRepository _transactionRepository;

        public OrderService(IOrderRepository orderRepository, IHoldingRepository holdingRepository, ITransactionRepository transactionRepository)
        {
            _orderRepository = orderRepository;
            _holdingRepository = holdingRepository;
            _transactionRepository = transactionRepository;
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

            var holding = await _holdingRepository.GetHoldingByUserAndStockAsync(request.UserId, request.StockId);

            if (holding == null)
            {
                holding = new Holding
                {
                    UserID = request.UserId,
                    StockID = request.StockId,
                    Qunatity = request.Quantity,
                    AveragePrice = request.Price
                };

                await _holdingRepository.AddHoldingAsync(holding);
            }
            else
            {
                int oldQuantity = holding.Qunatity;

                holding.Qunatity += request.Quantity;

                holding.AveragePrice = ((holding.AveragePrice * oldQuantity) + (request.Price * request.Quantity)) / holding.Qunatity;

                await _holdingRepository.UpdateHoldingAsync(holding);
            }

            var transaction = new Transaction
            {
                OrderId = order.OrderId,
                Amount = request.Quantity * request.Price,
                TransactionDate = DateTime.UtcNow
            };

            await _transactionRepository.AddTransactionAsync(transaction);

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

            if (order == null)
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

        public async Task<string> SellOrderAsync(CreateOrderDto request)
        {
            var holding = await _holdingRepository.GetHoldingByUserAndStockAsync(request.UserId, request.StockId);

            if (holding == null)
            {
                return "No Holding Found !";
            }

            if (holding.Qunatity < request.Quantity)
            {
                return "Insufficient Holdings !";
            }

            holding.Qunatity -= request.Quantity;

            await _holdingRepository.UpdateHoldingAsync(holding);

            var order = new Order
            {
                UserId = request.UserId,
                StockId = request.StockId,
                Quantity = request.Quantity,
                Price = request.Price,
                OrderType = OrderType.Sell,
                OrderStatus = OrderStatus.Completed,
                OrderDate = DateTime.UtcNow
            };

            await _orderRepository.AddOrderAsync(order);

            var transaction = new Transaction
            {
                OrderId = order.OrderId,
                Amount = request.Quantity * request.Price,
                TransactionDate = DateTime.UtcNow
            };

            await _transactionRepository.AddTransactionAsync(transaction);

            return "Sell Order Placed Successfully !";
        }

    }
}
