using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Core.Entities;

namespace TradeMaster.Infrastructure.Data
{
    public class TradeMasterDbContext : DbContext
    {
        public TradeMasterDbContext(DbContextOptions<TradeMasterDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Holding> Holdings { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Watchlist> Watchlists { get; set; }
    }
}
