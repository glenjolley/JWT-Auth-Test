
namespace JWTAuthTest.DataAccess
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> GetAsync<T, P>(string procName, P parameters);
        Task<int> SendAsync<P>(string procName, P parameters);
    }
}