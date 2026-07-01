
namespace TradeMaster.Application.DTOs
{
    public class OrderHistoryDto
    {
        public int OrderId { get; set; }

        public string StockName { get; set; } = string.Empty;

        public string OrderType { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string OrderStatus { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }
    }
}
