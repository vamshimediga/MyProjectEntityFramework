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
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserProfile>> GetAllAsync()
        {
            return await _context.Set<UserProfile>()
                .FromSqlRaw("EXEC GetAllUserProfiles")
                .ToListAsync();
        }

        public async Task<UserProfile> GetByIdAsync(int id)
        {
            var param = new SqlParameter("@UserProfileId", id);
            var result = await _context.Set<UserProfile>()
                .FromSqlRaw("EXEC GetByIdUserProfiles @UserProfileId", param)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task<int> AddAsync(UserProfile userProfile)
        {
            var fullNameParam = new SqlParameter("@FullName", userProfile.FullName ?? (object)DBNull.Value);
            var ageParam = new SqlParameter("@Age", userProfile.Age);
            var genderParam = new SqlParameter("@Gender", userProfile.Gender ?? (object)DBNull.Value);

            return await _context.Database.ExecuteSqlRawAsync(
                "EXEC InsertUserProfile @FullName, @Age, @Gender",
                fullNameParam, ageParam, genderParam);
        }

        public async Task<bool> UpdateAsync(UserProfile userProfile)
        {
            var idParam = new SqlParameter("@UserProfileId", userProfile.UserProfileId);
            var fullNameParam = new SqlParameter("@FullName", userProfile.FullName ?? (object)DBNull.Value);
            var ageParam = new SqlParameter("@Age", userProfile.Age);
            var genderParam = new SqlParameter("@Gender", userProfile.Gender ?? (object)DBNull.Value);

            var affectedRows = await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateUserProfile @UserProfileId, @FullName, @Age, @Gender",
                idParam, fullNameParam, ageParam, genderParam);

            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var idParam = new SqlParameter("@UserProfileId", id);

            var affectedRows = await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteUserProfile @UserProfileId", idParam);

            return affectedRows > 0;
        }
    }
}
