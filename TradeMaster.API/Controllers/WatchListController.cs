using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeMaster.Application.DTOs;
using TradeMaster.Application.Interfaces;
using TradeMaster.Infrastructure.Services;

namespace TradeMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WatchListController : ControllerBase
    {
        private readonly IWatchListService _watchListService;
        public WatchListController(IWatchListService watchListService)
        {
            _watchListService = watchListService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(AddWatchListDto dto)
        {
            var result = await _watchListService.AddToWatchlistAsync(dto);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserWatchlist(int userId)
        {
            var result = await _watchListService.GetUserWatchlistAsync(userId);
            return Ok(result);
        }

        [HttpDelete("{userId}/{stockId}")]
        public async Task<IActionResult> RemoveFromWatchlist(int userId, int stockId)
        {
            var result = await _watchListService.RemoveFromWatchlistAsync(userId, stockId);
            return Ok(result);
        }
    }
}
