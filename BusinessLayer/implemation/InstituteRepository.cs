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
    public class InstituteRepository : IInstitute
    {
        private readonly ApplicationDbContext _context;
        public InstituteRepository(ApplicationDbContext context) {
        _context = context;
        }
        public async Task<int> delete(int id)
        {
            SqlParameter instituteIdParam = new SqlParameter("@InstituteId", id);

            // Execute the stored procedure and get the number of rows affected
            var rowsAffected = await _context.Database
                .ExecuteSqlRawAsync("EXEC [dbo].[DeleteInstitute] @InstituteId", instituteIdParam);

            return rowsAffected; // Returns the number of rows affected
        }

        public async Task<Institute> GetInstituteByid(int id)
        {
            var sql = "EXEC [dbo].[GetInstituteById] @InstituteId = {0}";

            // Execute the stored procedure and get the result as a single entity
            Institute institute = _context.Institutes
                .FromSqlRaw(sql, id) // Pass the parameter to the SQL query
                .AsEnumerable()      // Move the query to the client side (client-side composition)
                .FirstOrDefault();   // Get the first institute or null if not found

            // Optionally, load related entities (like Students) if needed
            if (institute != null)
            {
                // Explicitly load the related Students navigation property
                await _context.Entry(institute)
                    .Collection(i => i.Students)
                    .LoadAsync();
            }

            return institute;
        }

        public async Task<List<Institute>> GetInstitutes()
        {
            List<Institute> institutes = await _context.Institutes
         .FromSqlRaw("EXEC [dbo].[GetAllInstitutes]")
         .ToListAsync();

            // Load related data
            foreach (var institute in institutes)
            {
                await _context.Entry(institute)
                    .Collection(i => i.Students) // Load related Students
                    .LoadAsync();
            }

            return institutes;
        }

        public async Task<int> insert(Institute institute)
        {
            SqlParameter instituteIdParam = new SqlParameter("@InstituteName", institute.InstituteName);

            var newInstituteId = _context.Database
                .ExecuteSqlRaw("EXEC [dbo].[InsertInstitute] @InstituteName", instituteIdParam);

            return newInstituteId;
        }

        public async Task<int> update(Institute institute)
        {
            SqlParameter instituteIdParam = new SqlParameter("@InstituteId", institute.InstituteId);
            SqlParameter instituteNameParam = new SqlParameter("@InstituteName", institute.InstituteName);

            var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[UpdateInstitute] @InstituteId, @InstituteName",
                instituteIdParam,
                instituteNameParam);

            return rowsAffected;
        }

    }
}
