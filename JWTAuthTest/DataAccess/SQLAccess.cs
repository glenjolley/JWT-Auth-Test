using Dapper;
using System.Data.SqlClient;

namespace JWTAuthTest.DataAccess;

public class SQLAccess : IDataAccess
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public SQLAccess(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("JWTTest");
    }

    public async Task<IEnumerable<T>> GetAsync<T, P>(string procName, P parameters)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            return await con.QueryAsync<T>(procName, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }

    public async Task<int> SendAsync<P>(string procName, P parameters)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            return await con.ExecuteAsync(procName, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
