using JWTAuthTest.Models;
using JWTAuthTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthTest.Controllers;

[Route("/auth")]
public class AuthController : Controller
{
    private readonly IUserService _userService;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public AuthController(
        IUserService userService, 
        IPasswordService passwordService,
        ITokenService tokenService)
    {
        _userService = userService;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest registerRequest)
    {
        User? user = await _userService.FindByUsernameAsync(registerRequest.Username);

        if (user != null)
        {
            return Unauthorized("Username already exists!");
        }

        var salt = await _passwordService.GenerateSalt();
        var hashedPassword = await _passwordService.HashPassword(registerRequest.Password, salt);

        var createdUser = await _userService.RegisterNewUser(
            new User
            {
                Username = registerRequest.Username,
                Password = hashedPassword,
                Salt = Convert.ToBase64String(salt),
                Role = Enums.Roles.USER
            });

        if (createdUser == null)
        {
            return BadRequest("That didn't work, not sure why");
        }

        var token = _tokenService.CreateToken(createdUser);

        return Ok(new AuthResponse { Token = token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        User? user = await _userService.FindByUsernameAsync(loginRequest.Username);

        if (user == null)
        {
            return Unauthorized("Invalid Credentials (User not found)");
        }

        var saltedPassword = user.Password;
        var hashedPassword = await _passwordService.HashPassword(loginRequest.Password, Convert.FromBase64String(user.Salt));

        if (hashedPassword != saltedPassword)
        {
            return Unauthorized("Invalid Credentials (Incorrect Password)");
        }

        var token = _tokenService.CreateToken(user);

        return Ok(new AuthResponse { Token = token });
    }
}
