using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Application.DTOs
{
    public class PortfolioSummaryDto
    {
        public int TotalHoldings { get; set; }

        public int TotalQuantity { get; set; }

        public decimal TotalInvestment { get; set; }

       // public decimal CurrentPortfolioValue { get; set; }
    }
}
