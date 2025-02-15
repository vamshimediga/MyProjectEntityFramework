using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class LawyerRepository : ILawyer
    {
        private readonly ApplicationDbContext _context;

        public LawyerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddLawyer(Lawyer lawyer)
        {
            var nameParam = new SqlParameter("@Name", lawyer.Name ?? (object)DBNull.Value);
            var specializationParam = new SqlParameter("@Specialization", lawyer.Specialization ?? (object)DBNull.Value);
            var experienceParam = new SqlParameter("@Experience", lawyer.Experience);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC InsertLawyer @Name, @Specialization, @Experience",
                nameParam, specializationParam, experienceParam
            );
        }

        public async Task DeleteLawyer(int id)
        {

            var lawyerIdParam = new SqlParameter("@LawyerID", id);

            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteLawyer @LawyerID", lawyerIdParam);
        }

        public async Task<IEnumerable<Lawyer>> GetAllLawyers()
        {
            IEnumerable<Lawyer> lawyers = await _context.Lawyers
                .FromSqlRaw("EXEC GetAllLawyers")
                .ToListAsync();
            foreach (Lawyer lawyer in lawyers)
            {
                // Use Collection() for loading collection navigation property (Posts)
                await _context.Entry(lawyer)
                    .Collection(l => l.Clients) // Correctly use Collection() for loading Posts
                    .LoadAsync();
            }
            return lawyers;
        }

        public async Task<Lawyer> GetLawyerById(int id)
        {
            var result = _context.Lawyers
        .FromSqlInterpolated($"EXEC GetLawyerById {id}")
        .AsEnumerable() // Ensure processing happens on the client side
        .FirstOrDefault();

            return result;
        }



        public async Task UpdateLawyer(Lawyer lawyer)
        {
            var sql = "EXEC UpdateLawyer @LawyerID, @Name, @Specialization, @Experience";

            var parameters = new[]
   {
        new SqlParameter("@LawyerID", lawyer.LawyerID),
        new SqlParameter("@Name", lawyer.Name),
        new SqlParameter("@Specialization", lawyer.Specialization),
        new SqlParameter("@Experience", lawyer.Experience)
    };

             await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }
    }
}
