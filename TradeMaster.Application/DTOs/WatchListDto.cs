using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Application.DTOs
{
    public class WatchListDto
    {
        public int WatchlistId { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public decimal CurrentPrice { get; set; }
    }
}
