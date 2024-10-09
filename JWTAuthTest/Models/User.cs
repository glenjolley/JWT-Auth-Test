using JWTAuthTest.Enums;

namespace JWTAuthTest.Models;

public class User
{
    public int UserId { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public string Salt { get; init; }
    public Roles Role { get; init; }
}
