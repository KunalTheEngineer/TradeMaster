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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Stock>()
                .Property(s => s.CurrentPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Holding>()
                .Property(h => h.AveragePrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);
        }

    }
}
