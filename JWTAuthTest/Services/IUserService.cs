using JWTAuthTest.Models;

namespace JWTAuthTest.Services
{
    public interface IUserService
    {
        Task<User> FindByUsernameAsync(string username);
        Task<User> RegisterNewUser(User user);
    }
}