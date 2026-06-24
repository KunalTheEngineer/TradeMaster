
namespace TradeMaster.Core.Entities
{
    public class User
    {
        public int USerID { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Holding> Holdings { get; set; } = new List<Holding>();

        public ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
    }
}
