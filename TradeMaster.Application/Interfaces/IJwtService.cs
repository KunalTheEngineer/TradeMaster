
using TradeMaster.Core.Entities;

namespace TradeMaster.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
