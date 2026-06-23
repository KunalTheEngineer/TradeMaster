using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Core.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}
