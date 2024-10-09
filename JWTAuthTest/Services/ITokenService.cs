using JWTAuthTest.Models;

namespace JWTAuthTest.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}