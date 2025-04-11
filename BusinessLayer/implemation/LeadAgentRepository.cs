using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    using System.Data.SqlClient;
    using System.Data;
    using Entities;
    using Microsoft.Extensions.Configuration;

    public class LeadAgentRepository : ILeadAgentRepository
    {
        private readonly string _connectionString;

        public LeadAgentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<LeadAgentDomainModel>> GetAll()
        {
            var leadAgents = new List<LeadAgentDomainModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"SELECT LeadAgentID, LeadName, AgentID FROM LeadAgents";
                SqlCommand cmd = new SqlCommand(query, conn);

                await conn.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    leadAgents.Add(new LeadAgentDomainModel
                    {
                        LeadAgentID = Convert.ToInt32(reader["LeadAgentID"]),
                        LeadName = reader["LeadName"].ToString(),
                        AgentID = Convert.ToInt32(reader["AgentID"])
                    });
                }
            }

            return leadAgents;
        }

        public async Task<LeadAgentDomainModel> GetByIdAsync(int id)
        {
            LeadAgentDomainModel leadAgent = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"SELECT LeadAgentID, LeadName, AgentID FROM LeadAgents WHERE LeadAgentID = @LeadAgentID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LeadAgentID", id);

                await conn.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    leadAgent = new LeadAgentDomainModel
                    {
                        LeadAgentID = Convert.ToInt32(reader["LeadAgentID"]),
                        LeadName = reader["LeadName"].ToString(),
                        AgentID = Convert.ToInt32(reader["AgentID"])
                    };
                }
            }

            return leadAgent;
        }

        public async Task<int> Add(LeadAgentDomainModel leadAgent)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO LeadAgents (LeadName, AgentID) VALUES (@LeadName, @AgentID)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LeadName", leadAgent.LeadName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AgentID", leadAgent.AgentID);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return 1;
            }
        }

        public async Task<bool> Update(LeadAgentDomainModel leadAgent)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE LeadAgents SET LeadName = @LeadName, AgentID = @AgentID WHERE LeadAgentID = @LeadAgentID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LeadName", leadAgent.LeadName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AgentID", leadAgent.AgentID);
                cmd.Parameters.AddWithValue("@LeadAgentID", leadAgent.LeadAgentID);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return true;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM LeadAgents WHERE LeadAgentID = @LeadAgentID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LeadAgentID", id);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return true;
            }
        }
    }

}
