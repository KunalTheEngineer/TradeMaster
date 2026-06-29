using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TradeMaster.Application.Interfaces;

namespace TradeMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpGet("summary/{userId}")]
        public async Task<IActionResult> GetPortfolioSummary(int userId)
        {
            var summary = await _portfolioService.GetPortfolioSummaryAsync(userId);

            return Ok(summary);
        }

        [HttpGet("profit-loss/{userId}")]
        public async Task<IActionResult> GetProfitLoss(int userId)
        {
            var result = await _portfolioService.GetProfitLossAsync(userId);

            return Ok(result);
        }
    }
}
