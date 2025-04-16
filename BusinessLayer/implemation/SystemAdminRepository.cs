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
    public class SystemAdminRepository : ISystemAdmin
    {
        private readonly ApplicationDbContext _context;

        public SystemAdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SystemAdminDomainModel>> GetAllAsync()
        {
            return await _context.SystemAdmins
                .FromSqlRaw("EXEC GetAllSystemAdmins")
                .ToListAsync();
        }

        public async Task<SystemAdminDomainModel> GetByIdAsync(int id)
        {
            var param = new SqlParameter("@SystemAdminId", id);

            var result =  _context.SystemAdmins
                .FromSqlRaw("EXEC GetSystemAdminById @SystemAdminId", param)
                .AsEnumerable() // 👉 This moves the rest of the query to client-side
                .FirstOrDefault();

            return result;
        }


        public async Task<int> InsertAsync(SystemAdminDomainModel admin)
        {
            var nameParam = new SqlParameter("@AdminName", admin.AdminName ?? (object)DBNull.Value);
            var devIdParam = new SqlParameter("@DeveloperId", admin.DeveloperId);

            var result = await _context.Database.ExecuteSqlRawAsync("EXEC InsertSystemAdmin @AdminName, @DeveloperId", nameParam, devIdParam);
            return result; // 1 if inserted
        }

        public async Task<bool> UpdateAsync(SystemAdminDomainModel admin)
        {
            var idParam = new SqlParameter("@SystemAdminId", admin.SystemAdminId);
            var nameParam = new SqlParameter("@AdminName", admin.AdminName ?? (object)DBNull.Value);
            var devIdParam = new SqlParameter("@DeveloperId", admin.DeveloperId);

            var result = await _context.Database.ExecuteSqlRawAsync("EXEC UpdateSystemAdmin @SystemAdminId, @AdminName, @DeveloperId", idParam, nameParam, devIdParam);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var param = new SqlParameter("@SystemAdminId", id);
            var result = await _context.Database.ExecuteSqlRawAsync("EXEC DeleteSystemAdmin @SystemAdminId", param);
            return result > 0;
        }
    }
}
