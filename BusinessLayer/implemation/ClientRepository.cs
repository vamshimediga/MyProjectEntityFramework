using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class ClientRepository : IClient
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetClients()
        {
            List<Client> clients= await _context.Clients.FromSqlRaw("EXEC GetAllClients").ToListAsync();
            foreach (Client client in clients)
            {
                // Use Collection() for loading collection navigation property (Posts)
                await _context.Entry(client)
                    .Reference(c =>c.Lawyer) // Correctly use Reference() for loading Posts
                    .LoadAsync();
            }
            return clients;
        }

        public async Task<Client> GetClient(int id)
        {
            var param = new SqlParameter("@ClientID", id);
            return  _context.Clients
                .FromSqlRaw("EXEC GetClientById @ClientID", param)
                .AsEnumerable() // Ensures further filtering is done client-side
                .FirstOrDefault();
        }

        public async Task insert(Client client)
        {
            var parameters = new[]
            {
                new SqlParameter("@LawyerID", client.LawyerID),
                new SqlParameter("@Name", client.Name ?? (object)DBNull.Value),
                new SqlParameter("@CaseType", client.CaseType ?? (object)DBNull.Value)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC InsertClient @LawyerID, @Name, @CaseType", parameters);
        }

        public async Task update(Client client)
        {
            var parameters = new[]
            {
                new SqlParameter("@ClientID", client.ClientID),
                new SqlParameter("@LawyerID", client.LawyerID),
                new SqlParameter("@Name", client.Name ?? (object)DBNull.Value),
                new SqlParameter("@CaseType", client.CaseType ?? (object)DBNull.Value)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC UpdateClient @ClientID, @LawyerID, @Name, @CaseType", parameters);
        }

        public async Task delete(int id)
        {
            var param = new SqlParameter("@ClientID", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteClient @ClientID", param);
        }

    }
}
