using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeMaster.Application.Interfaces;

namespace TradeMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class HoldingController : ControllerBase
    {
        private readonly IHoldingService _holdingService;

        public HoldingController(IHoldingService holdingService)
        {
            _holdingService = holdingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHoldings()
        {
            var result = await _holdingService.GetAllHoldingsAsync();

            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetHoldingsByUserOd(int userId)
        {
            var result = await _holdingService.GetHoldingsByUserIdAsync(userId);

            return Ok(result);
        }
    }
}
