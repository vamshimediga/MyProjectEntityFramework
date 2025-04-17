using BusinessLayer;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class TeamLeadAdoRepository : ITeamLeadRepository
{
    private readonly string _connectionString;

    public TeamLeadAdoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<List<TeamLead>> GetAllTeamLeadsAsync()
    {
        var leads = new List<TeamLead>();

        using (var conn = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("SELECT * FROM TeamLeads", conn))
        {
            await conn.OpenAsync();
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    leads.Add(new TeamLead()
                    {
                        TeamLeadId = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }
        }

        return leads;
    }

    public async Task<TeamLead> GetTeamLeadByIdAsync(int id)
    {
        TeamLead lead = null;

        using (var conn = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("SELECT * FROM TeamLeads WHERE TeamLeadId = @Id", conn))
        {
            cmd.Parameters.AddWithValue("@Id", id);
            await conn.OpenAsync();

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    lead = new TeamLead
                    {
                        TeamLeadId = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
            }
        }

        return lead;
    }

    public async Task<int> AddTeamLeadAsync(TeamLead teamLead)
    {
        using (var conn = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("INSERT INTO TeamLeads (Name) VALUES (@Name)", conn))
        {
            cmd.Parameters.AddWithValue("@Name", teamLead.Name);
            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }
    }

    public async Task<bool> UpdateTeamLeadAsync(TeamLead teamLead)
    {
        using (var conn = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("UPDATE TeamLeads SET Name = @Name WHERE TeamLeadId = @Id", conn))
        {
            cmd.Parameters.AddWithValue("@Name", teamLead.Name);
            cmd.Parameters.AddWithValue("@Id", teamLead.TeamLeadId);
            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }

    public async Task<bool> DeleteTeamLeadAsync(int id)
    {
        using (var conn = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("DELETE FROM TeamLeads WHERE TeamLeadId = @Id", conn))
        {
            cmd.Parameters.AddWithValue("@Id", id);
            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}
