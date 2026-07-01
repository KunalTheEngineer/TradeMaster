using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Application.DTOs
{
    public class AddWatchListDto
    {
        public int UserId { get; set; }

        public int StockId { get; set; }
    }
}
