
namespace TradeMaster.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string email);
    }
}
