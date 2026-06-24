using TradeMaster.Core.Entities;

namespace TradeMaster.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);

        Task AddUserAsync(User user);
    }
}
