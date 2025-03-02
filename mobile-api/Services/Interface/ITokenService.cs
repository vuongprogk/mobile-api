using mobile_api.Models;

namespace mobile_api.Services.Interface
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
