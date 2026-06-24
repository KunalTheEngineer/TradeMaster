
namespace TradeMaster.Application.DTOs
{
    public class HoldingResponseDto
    {
        public int HoldingId { get; set; }

        public int UserId { get; set; }

        public int StockId { get; set; }

        public int Quantity { get; set; }

        public decimal AveragePrice { get; set; }
    }
}
