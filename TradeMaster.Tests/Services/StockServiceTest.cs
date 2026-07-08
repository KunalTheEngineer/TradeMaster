using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

using TradeMaster.Application.Interfaces;
using TradeMaster.Core.Interfaces;
using TradeMaster.Infrastructure.Services;
using TradeMaster.Core.Entities;
using TradeMaster.Core.QueryParameters;
using TradeMaster.Application.DTOs;

namespace TradeMaster.Tests.Services
{
    public class StockServiceTest
    {
        private readonly Mock<IStockRepository> _stockRepositoryMock;

        private readonly Mock<ILogger<StockService>> _loggerMock;

        private readonly StockService _stockService;

        public StockServiceTest()
        {
            _stockRepositoryMock = new Mock<IStockRepository>();

            _loggerMock = new Mock<ILogger<StockService>>();

            _stockService = new StockService(
                _stockRepositoryMock.Object,
                _loggerMock.Object);
        }

        [Fact] // BASIC UNIT TESTING
        public async Task GetAllStockAsync_ShouldReturnPagedStocks_WhenStocksExist()
        {
            //ARRANGE

            var query = new StockQueryParameters
            {
                Page = 1,
                PageSize = 10
            };

            var stocks = new List<Stock>
            {
                new Stock
                {
                    StockID = 1,
                    Symbol = "TCS",
                    CompanyName = "Tata Consultancy Services",
                    CurrentPrice = 3500
                },

                new Stock
                {
                    StockID = 2,
                    Symbol = "INFY",
                    CompanyName = "Infosys",
                    CurrentPrice = 1500
                }
            };

            _stockRepositoryMock
             .Setup(x => x.GetAllStocksAsync(query))
             .ReturnsAsync(stocks);

            _stockRepositoryMock
                .Setup(x => x.GetTotalStockCountAsync())
                .ReturnsAsync(2);

            // Act

            var result = await _stockService.GetAllStockAsync(query);

            // Assert

            Assert.NotNull(result);

            Assert.Equal(2, result.Data.Count);

            Assert.Equal(1, result.Page);

            Assert.Equal(10, result.PageSize);

            Assert.Equal(2, result.TotalRecords);

            Assert.Equal(1, result.TotalPages);

            _stockRepositoryMock.Verify(
            x => x.GetAllStocksAsync(query),
            Times.Once);

            _stockRepositoryMock.Verify(
                x => x.GetTotalStockCountAsync(),
                Times.Once);

        }

        [Fact] // TESTING EMPTY RESULTS
        public async Task GetAllStockAsync_ShouldReturnEmptyList_WhenNoStocksExist()
        {
            // ARRANGE

            var query = new StockQueryParameters
            {
                Page = 1,
                PageSize = 10
            };

            var stocks = new List<Stock>();

            _stockRepositoryMock
                .Setup(x => x.GetAllStocksAsync(query))
                .ReturnsAsync(stocks);

            _stockRepositoryMock
                .Setup(x => x.GetTotalStockCountAsync())
                .ReturnsAsync(0);

            // Act

            var result = await _stockService.GetAllStockAsync(query);

            // Assert

            Assert.NotNull(result);

            Assert.Empty(result.Data);

            Assert.Equal(1, result.Page);

            Assert.Equal(10, result.PageSize);

            Assert.Equal(0, result.TotalRecords);

            Assert.Equal(0, result.TotalPages);

            _stockRepositoryMock.Verify(
                x => x.GetAllStocksAsync(query),
                Times.Once);

            _stockRepositoryMock.Verify(
                x => x.GetTotalStockCountAsync(),
                Times.Once);
        }

    }
}
