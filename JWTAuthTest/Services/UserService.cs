using JWTAuthTest.DataAccess;
using JWTAuthTest.Models;

namespace JWTAuthTest.Services;

public class UserService : IUserService
{
    private readonly IDataAccess _dataAccess;

    public UserService(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<User> FindByUsernameAsync(string username)
    {
        var user = await _dataAccess.GetAsync<User, dynamic>("FindByUsername", new { Username = username });
        return user.FirstOrDefault();
    }

    public async Task<User> RegisterNewUser(User user)
    {
        await _dataAccess.SendAsync<User>("CreateNewUser", user);
        var createdUser = await _dataAccess.GetAsync<User, dynamic>("FindByUsername", new { Username = user.Username });
        return createdUser.FirstOrDefault();
    }
}
