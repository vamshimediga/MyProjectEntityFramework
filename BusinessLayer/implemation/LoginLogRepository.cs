using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Data;
// Repositories/LoginLogRepository.cs
using System.Threading.Tasks;
using Dapper;
using System.Data;
namespace BusinessLayer.implemation
{
    public class LoginLogRepository : ILoginLogRepository
    {
        private readonly string _connectionString;

        public LoginLogRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task LogLoginAsync(LoginLog log)
        {
            var query = @"INSERT INTO LoginLogs (LogId, Username, IPAddress, UserAgent, LoginTime)
                  VALUES (@LogId, @Username, @IPAddress, @UserAgent, @LoginTime)";

            var parameters = new DynamicParameters();
            parameters.Add("@LogId", log.LogId);
            parameters.Add("@Username", log.Username);
            parameters.Add("@IPAddress", log.IPAddress);
            parameters.Add("@UserAgent", log.UserAgent);
            parameters.Add("@LoginTime", log.LoginTime);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

    }

}
