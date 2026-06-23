using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Core.Entities
{
    public class Watchlist
    {
        public int WatchlistId { get; set; }

        public int UserId { get; set; }

        public int StockId { get; set; }
    }
}
