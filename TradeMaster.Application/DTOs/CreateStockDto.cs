
namespace TradeMaster.Application.DTOs
{
    public class CreateStockDto
    {
        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public Decimal CurrentPrice { get; set; }
    }
}
