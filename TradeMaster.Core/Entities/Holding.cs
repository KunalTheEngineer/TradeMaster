using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Core.Entities
{
    public class Holding
    {
        public int HoldingID { get; set; }

        public int UserID { get; set; }

        public int StockID { get; set; }

        public int Qunatity { get; set; }

        public decimal AveragePrice { get; set; }
    }
}
