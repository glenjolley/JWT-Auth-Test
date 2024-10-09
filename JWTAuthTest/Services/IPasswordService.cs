
namespace JWTAuthTest.Services
{
    public interface IPasswordService
    {
        Task<byte[]> GenerateSalt();
        Task<string> HashPassword(string password, byte[] salt);
    }
}