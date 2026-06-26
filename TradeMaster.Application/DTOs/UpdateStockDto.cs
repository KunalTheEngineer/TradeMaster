
namespace TradeMaster.Application.DTOs
{
    public class UpdateStockDto
    {
        public string Symbol {  get; set; } = string.Empty;

        public string CompnayName {  get; set; } = string.Empty;

        public decimal CurrentPrice { get; set; }
    }
}
