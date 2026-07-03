
using Microsoft.Extensions.Logging;
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
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, IHoldingRepository holdingRepository, ITransactionRepository transactionRepository, ILogger<OrderService> logger, IStockRepository stockRepository)
        {
            _orderRepository = orderRepository;
            _holdingRepository = holdingRepository;
            _transactionRepository = transactionRepository;
            _stockRepository = stockRepository;
            _logger = logger;
            
        }

        public async Task<string> BuyOrderAsync(CreateOrderDto request)
        {
           
            var stock = await _stockRepository.GetStockByIdAsync(request.StockId);

            if (stock == null)
            {
                _logger.LogWarning(
                    "Buy order failed. Stock {StockId} not found for User {UserId}.",
                    request.StockId,
                    request.UserId);

                throw new KeyNotFoundException("Stock not found.");
            }

            _logger.LogInformation(
           "User {UserId} is placing a buy order for Stock {StockId}. Quantity: {Quantity}, Price: {Price}.",
           request.UserId,
           request.StockId,
           request.Quantity,
           request.Price);

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

            _logger.LogInformation(
     "BUY ORDER SUCCESS | User: {UserId} | Stock: {Symbol} | Quantity: {Quantity} | Price: {Price} | Total: {Amount}",
     request.UserId,
     stock.Symbol,
     request.Quantity,
     request.Price,
     request.Quantity * request.Price);

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

            var stock = await _stockRepository.GetStockByIdAsync(request.StockId);

            if (stock == null)
            {
                _logger.LogWarning(
                    "Sell order failed. Stock {StockId} not found for User {UserId}.",
                    request.StockId,
                    request.UserId);

                throw new KeyNotFoundException("Stock not found.");
            }

            _logger.LogInformation(
            "User {UserId} is placing a sell order for {Symbol}. Quantity: {Quantity}, Price: {Price}.",
            request.UserId,
            stock.Symbol,
            request.Quantity,
            request.Price);

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


            _logger.LogInformation(
            "SELL ORDER SUCCESS | User: {UserId} | Stock: {Symbol} | Quantity: {Quantity} | Price: {Price} | Total: {Amount}",
             request.UserId,
              stock.Symbol,
             request.Quantity,
             request.Price,
             request.Quantity * request.Price);

            return "Sell Order Placed Successfully !";
        }

        public async Task<List<OrderHistoryDto>> GetOrderHistoryAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

            return orders.Select(order => new OrderHistoryDto
            {
                OrderId = order.OrderId,
                StockName = order.Stock.CompanyName,
                OrderType = order.OrderType.ToString(),
                Quantity = order.Quantity,
                Price = order.Price,
                OrderStatus = order.OrderStatus.ToString(),
                OrderDate = order.OrderDate
            }).ToList();
        }

    }
}
