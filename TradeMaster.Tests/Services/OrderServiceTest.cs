using Microsoft.Extensions.Logging;
using Moq;
using TradeMaster.Application.DTOs;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;
using TradeMaster.Infrastructure.Services;

namespace TradeMaster.Tests.Services
{
    public class OrderServiceTest
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IHoldingRepository> _holdingRepositoryMock;
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly Mock<IStockRepository> _stockRepositoryMock;
        private readonly Mock<ILogger<OrderService>> _loggerMock;

        private readonly OrderService _orderService;

        public OrderServiceTest()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();

            _holdingRepositoryMock = new Mock<IHoldingRepository>();

            _transactionRepositoryMock = new Mock<ITransactionRepository>();

            _stockRepositoryMock = new Mock<IStockRepository>();

            _loggerMock = new Mock<ILogger<OrderService>>();

            _orderService = new OrderService(
                _orderRepositoryMock.Object,
                _holdingRepositoryMock.Object,
                _transactionRepositoryMock.Object,
                _loggerMock.Object,
                _stockRepositoryMock.Object);
        }

        [Fact] // NEW HOLDING CREATED TEST
        public async Task BuyOrderAsync_ShouldCreateNewHolding_WhenHoldingDoesNotExist()
        {
            // Arrange

            var request = new CreateOrderDto
            {
                UserId = 1,
                StockId = 101,
                Quantity = 10,
                Price = 100
            };

            var stock = new Stock
            {
                StockID = 101,
                Symbol = "TCS",
                CurrentPrice = 100
            };

            _stockRepositoryMock
                .Setup(x => x.GetStockByIdAsync(request.StockId))
                .ReturnsAsync(stock);

            _holdingRepositoryMock
                .Setup(x => x.GetHoldingByUserAndStockAsync(request.UserId, request.StockId))
                .ReturnsAsync((Holding)null);

            _orderRepositoryMock
                .Setup(x => x.AddOrderAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            _holdingRepositoryMock
                .Setup(x => x.AddHoldingAsync(It.IsAny<Holding>()))
                .Returns(Task.CompletedTask);

            _transactionRepositoryMock
                .Setup(x => x.AddTransactionAsync(It.IsAny<Transaction>()))
                .Returns(Task.CompletedTask);

            // Act

            var result = await _orderService.BuyOrderAsync(request);

            // Assert

            Assert.Equal("Buy Order Placed Successfully !", result);

            _stockRepositoryMock.Verify(
                x => x.GetStockByIdAsync(request.StockId),
                Times.Once);

            _orderRepositoryMock.Verify(
                x => x.AddOrderAsync(It.IsAny<Order>()),
                Times.Once);

            _holdingRepositoryMock.Verify(
                x => x.AddHoldingAsync(It.IsAny<Holding>()),
                Times.Once);

            _transactionRepositoryMock.Verify(
                x => x.AddTransactionAsync(It.IsAny<Transaction>()),
                Times.Once);
        }

        [Fact] // UPDATE EXISITING HOLDING TEST
        public async Task BuyOrderAsync_ShouldUpdateExistingHolding_WhenHoldingAlreadyExists()
        {
            // Arrange

            var request = new CreateOrderDto
            {
                UserId = 1,
                StockId = 101,
                Quantity = 10,
                Price = 100
            };

            var stock = new Stock
            {
                StockID = 101,
                Symbol = "TCS",
                CurrentPrice = 100
            };

            var existingHolding = new Holding
            {
                UserID = 1,
                StockID = 101,
                Qunatity = 20,
                AveragePrice = 80
            };

            _stockRepositoryMock
                .Setup(x => x.GetStockByIdAsync(request.StockId))
                .ReturnsAsync(stock);

            _holdingRepositoryMock
                .Setup(x => x.GetHoldingByUserAndStockAsync(request.UserId, request.StockId))
                .ReturnsAsync(existingHolding);

            _orderRepositoryMock
                .Setup(x => x.AddOrderAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            _holdingRepositoryMock
                .Setup(x => x.UpdateHoldingAsync(It.IsAny<Holding>()))
                .Returns(Task.CompletedTask);

            _transactionRepositoryMock
                .Setup(x => x.AddTransactionAsync(It.IsAny<Transaction>()))
                .Returns(Task.CompletedTask);

            // Act

            var result = await _orderService.BuyOrderAsync(request);

            // Assert

            Assert.Equal("Buy Order Placed Successfully !", result);

            Assert.Equal(30, existingHolding.Qunatity);

            Assert.Equal(86.67m, Math.Round(existingHolding.AveragePrice, 2));

            _holdingRepositoryMock.Verify(
                x => x.UpdateHoldingAsync(It.IsAny<Holding>()),
                Times.Once);

            _holdingRepositoryMock.Verify(
                x => x.AddHoldingAsync(It.IsAny<Holding>()),
                Times.Never);
        }

        [Fact] // STOCK NOT FOUND TEST
        public async Task BuyOrderAsync_ShouldThrowKeyNotFoundException_WhenStockDoesNotExist()
        {
            // Arrange

            var request = new CreateOrderDto
            {
                UserId = 1,
                StockId = 999,
                Quantity = 10,
                Price = 100
            };

            _stockRepositoryMock
                .Setup(x => x.GetStockByIdAsync(request.StockId))
                .ReturnsAsync((Stock)null);

            // Act & Assert

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _orderService.BuyOrderAsync(request));

            Assert.Equal("Stock not found.", exception.Message);

            _stockRepositoryMock.Verify(
                x => x.GetStockByIdAsync(request.StockId),
                Times.Once);

            _orderRepositoryMock.Verify(
                x => x.AddOrderAsync(It.IsAny<Order>()),
                Times.Never);

            _holdingRepositoryMock.Verify(
                x => x.AddHoldingAsync(It.IsAny<Holding>()),
                Times.Never);

            _transactionRepositoryMock.Verify(
                x => x.AddTransactionAsync(It.IsAny<Transaction>()),
                Times.Never);
        }

        [Fact] // SELL ORDER SUCCESSFUL TEST
        public async Task SellOrderAsync_ShouldReturnSuccess_WhenSellOrderIsPlacedSuccessfully()
        {
            // Arrange

            var request = new CreateOrderDto
            {
                UserId = 1,
                StockId = 101,
                Quantity = 5,
                Price = 100
            };

            var stock = new Stock
            {
                StockID = 101,
                Symbol = "TCS",
                CurrentPrice = 100
            };

            var holding = new Holding
            {
                UserID = 1,
                StockID = 101,
                Qunatity = 20,
                AveragePrice = 80
            };

            _stockRepositoryMock
                .Setup(x => x.GetStockByIdAsync(request.StockId))
                .ReturnsAsync(stock);

            _holdingRepositoryMock
                .Setup(x => x.GetHoldingByUserAndStockAsync(request.UserId, request.StockId))
                .ReturnsAsync(holding);

            _holdingRepositoryMock
                .Setup(x => x.UpdateHoldingAsync(It.IsAny<Holding>()))
                .Returns(Task.CompletedTask);

            _orderRepositoryMock
                .Setup(x => x.AddOrderAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            _transactionRepositoryMock
                .Setup(x => x.AddTransactionAsync(It.IsAny<Transaction>()))
                .Returns(Task.CompletedTask);

            // Act

            var result = await _orderService.SellOrderAsync(request);

            // Assert

            Assert.Equal("Sell Order Placed Successfully !", result);

            Assert.Equal(15, holding.Qunatity);

            _holdingRepositoryMock.Verify(
                x => x.UpdateHoldingAsync(It.IsAny<Holding>()),
                Times.Once);

            _orderRepositoryMock.Verify(
                x => x.AddOrderAsync(It.IsAny<Order>()),
                Times.Once);

            _transactionRepositoryMock.Verify(
                x => x.AddTransactionAsync(It.IsAny<Transaction>()),
                Times.Once);
        }

        [Fact] // NO HOLDING FOUND TEST
        public async Task SellOrderAsync_ShouldReturnNoHoldingFound_WhenHoldingDoesNotExist()
        {
            // Arrange

            var request = new CreateOrderDto
            {
                UserId = 1,
                StockId = 101,
                Quantity = 5,
                Price = 100
            };

            var stock = new Stock
            {
                StockID = 101,
                Symbol = "TCS"
            };

            _stockRepositoryMock
                .Setup(x => x.GetStockByIdAsync(request.StockId))
                .ReturnsAsync(stock);

            _holdingRepositoryMock
                .Setup(x => x.GetHoldingByUserAndStockAsync(request.UserId, request.StockId))
                .ReturnsAsync((Holding)null);

            // Act

            var result = await _orderService.SellOrderAsync(request);

            // Assert

            Assert.Equal("No Holding Found !", result);

            _orderRepositoryMock.Verify(
                x => x.AddOrderAsync(It.IsAny<Order>()),
                Times.Never);

            _transactionRepositoryMock.Verify(
                x => x.AddTransactionAsync(It.IsAny<Transaction>()),
                Times.Never);
        }

        [Fact] // INSUFFICIENT HOLDINGS
        public async Task SellOrderAsync_ShouldReturnInsufficientHoldings_WhenQuantityIsGreaterThanHolding()
        {
            // Arrange

            var request = new CreateOrderDto
            {
                UserId = 1,
                StockId = 101,
                Quantity = 20,
                Price = 100
            };

            var stock = new Stock
            {
                StockID = 101,
                Symbol = "TCS"
            };

            var holding = new Holding
            {
                UserID = 1,
                StockID = 101,
                Qunatity = 10,
                AveragePrice = 90
            };

            _stockRepositoryMock
                .Setup(x => x.GetStockByIdAsync(request.StockId))
                .ReturnsAsync(stock);

            _holdingRepositoryMock
                .Setup(x => x.GetHoldingByUserAndStockAsync(request.UserId, request.StockId))
                .ReturnsAsync(holding);

            // Act

            var result = await _orderService.SellOrderAsync(request);

            // Assert

            Assert.Equal("Insufficient Holdings !", result);

            _holdingRepositoryMock.Verify(
                x => x.UpdateHoldingAsync(It.IsAny<Holding>()),
                Times.Never);

            _orderRepositoryMock.Verify(
                x => x.AddOrderAsync(It.IsAny<Order>()),
                Times.Never);

            _transactionRepositoryMock.Verify(
                x => x.AddTransactionAsync(It.IsAny<Transaction>()),
                Times.Never);
        }

        [Fact] // STOCK NOT FOUND TEST
        public async Task SellOrderAsync_ShouldThrowKeyNotFoundException_WhenStockDoesNotExist()
        {
            // Arrange

            var request = new CreateOrderDto
            {
                UserId = 1,
                StockId = 999,
                Quantity = 5,
                Price = 100
            };

            _stockRepositoryMock
                .Setup(x => x.GetStockByIdAsync(request.StockId))
                .ReturnsAsync((Stock)null);

            // Act & Assert

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _orderService.SellOrderAsync(request));

            Assert.Equal("Stock not found.", exception.Message);

            _orderRepositoryMock.Verify(
                x => x.AddOrderAsync(It.IsAny<Order>()),
                Times.Never);

            _transactionRepositoryMock.Verify(
                x => x.AddTransactionAsync(It.IsAny<Transaction>()),
                Times.Never);
        }
    }
}
