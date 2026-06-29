
namespace TradeMaster.Application.DTOs
{
    public class ProfitLossDto
    {
        public string StockName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal AveragePrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal TotalInvestment { get; set; }

        public decimal CurrentValue { get; set; }

        public decimal ProfitLoss { get; set; }
    }
}
