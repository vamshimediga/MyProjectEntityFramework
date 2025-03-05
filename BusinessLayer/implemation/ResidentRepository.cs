using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class ResidentRepository : IResident
    {
        private readonly ApplicationDbContext _context;
        public ResidentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task Delete(int residentId)
        {
            var residentIdParam = new SqlParameter("@ResidentID", residentId);
             await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[DeleteResident] @ResidentID", residentIdParam);
        }

        public async Task<Resident> GetResidentByIdAsync(int residentId)
        {
            var residentIdParam = new SqlParameter("@ResidentID", residentId);

            return _context.Residents
                .FromSqlRaw("EXEC [dbo].[GetResidentById] @ResidentID", residentIdParam)
                .AsEnumerable()  // Move query execution to the client-side
                .FirstOrDefault();
        }
        public async Task<List<Resident>> GetResidentsAsync()
        {
            List<Resident> residents = await _context.Residents.FromSqlRaw("EXEC GetAllResidents").ToListAsync();


            foreach (var resident in residents)
            {
                // Load related books for each author
                await _context.Entry(resident)
                    .Reference(r =>r.Apartment)  // Include the related Books collection
                    .LoadAsync();
            }
            return residents;

        }

        public Task Insert(Resident resident)
        {
            throw new NotImplementedException();
        }

        public Task Update(Resident resident)
        {
            throw new NotImplementedException();
        }
    }
}
