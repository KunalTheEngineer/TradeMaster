
namespace TradeMaster.Application.DTOs
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int StockId { get; set; }

        public string OrderType { get; set; } = string.Empty;

        public string OrderStatus { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
