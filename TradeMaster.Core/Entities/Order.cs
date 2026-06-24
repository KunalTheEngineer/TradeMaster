using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Core.Enums;

namespace TradeMaster.Core.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int StockId { get; set; }

        public OrderType OrderType { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;

        public Stock Stock { get; set; } = null!;
    }
}
