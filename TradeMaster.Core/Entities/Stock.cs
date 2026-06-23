using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Core.Entities
{
    public class Stock
    {
        public int StockID { get; set; }

        public string Symbol {  get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public decimal CurrentPrice { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
