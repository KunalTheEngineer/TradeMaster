using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeMaster.Application.DTOs;
using TradeMaster.Application.Interfaces;
using TradeMaster.Core.QueryParameters;

namespace TradeMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddStock(CreateStockDto request)
        {
            var result = await _stockService.AddStockAsync(request);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStocks([FromQuery] StockQueryParameters query)
        {
            var stocks = await _stockService.GetAllStockAsync(query);

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById(int id)
        {
            var result = await _stockService.GetStockByIdAsync(id);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, UpdateStockDto request)
        {
            var result = await _stockService.UpdateStockAsync(id, request);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var result = await _stockService.DeleteStockAsync(id);

            return Ok(result);
        }
    }
}
