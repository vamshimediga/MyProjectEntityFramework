using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class AgentRepository : IAgentRepository
    {
        private readonly string _connectionString;

        public AgentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<AgentDomainModel>> GetAllAsync()
        {
            List<AgentDomainModel> agents = new List<AgentDomainModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT AgentID, Name FROM Agents";
                SqlCommand cmd = new SqlCommand(query, conn);
                await conn.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    agents.Add(new AgentDomainModel
                    {
                        AgentID = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }
            return agents;
        }

        public async Task<AgentDomainModel> GetByIdAsync(int id)
        {
            AgentDomainModel agent = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT AgentID, Name FROM Agents WHERE AgentID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                await conn.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    agent = new AgentDomainModel
                    {
                        AgentID = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
            }
            return agent;
        }

        public async Task<int> InsertAsync(AgentDomainModel agent)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Agents (Name) OUTPUT INSERTED.AgentID VALUES (@name)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", agent.Name);
                await conn.OpenAsync();
                return  agent.AgentID = (int)await cmd.ExecuteScalarAsync();
            }
          
        }

        public async Task<bool> UpdateAsync(AgentDomainModel agent)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Agents SET Name = @name WHERE AgentID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", agent.Name);
                cmd.Parameters.AddWithValue("@id", agent.AgentID);
                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
          
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Agents WHERE AgentID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
}
