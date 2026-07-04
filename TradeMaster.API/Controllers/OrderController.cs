using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeMaster.Application.DTOs;
using TradeMaster.Application.Interfaces;

namespace TradeMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyOrder(CreateOrderDto request)
        {
            var result = await _orderService.BuyOrderAsync(request);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);

            return Ok(result);
        }

        [HttpPost("sell")]
        public async Task<IActionResult> SellOrder(CreateOrderDto request)
        {
            var result = await _orderService.SellOrderAsync(request);

            return Ok(result);
        }

        [HttpGet("history/{userId}")]
        public async Task<IActionResult> GetOrderHistory(int userId)
        {
            var history = await _orderService.GetOrderHistoryAsync(userId);

            return Ok(history);
        }
    }
}
