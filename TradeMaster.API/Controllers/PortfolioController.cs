using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TradeMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        [Authorize]
        [HttpGet]

        public IActionResult GetPortfolio()
        {
            return Ok("Welcome To TradeMaster Portfolio");
        }
    }
}
