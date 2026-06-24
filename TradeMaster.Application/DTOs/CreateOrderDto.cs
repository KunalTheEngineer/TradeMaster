
namespace TradeMaster.Application.DTOs
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }

        public int StockId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
