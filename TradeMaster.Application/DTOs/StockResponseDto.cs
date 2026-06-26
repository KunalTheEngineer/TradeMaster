
using Microsoft.VisualBasic;

namespace TradeMaster.Application.DTOs
{
    public class StockResponseDto
    {
        public int StockId { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public decimal CurrentPrice { get; set; }
    }
}
