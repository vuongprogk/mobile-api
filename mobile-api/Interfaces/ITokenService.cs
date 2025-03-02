using mobile_api.Models;

namespace mobile_api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
