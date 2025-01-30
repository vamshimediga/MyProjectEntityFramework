using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class UsersRepository : IUsers
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> delete(int id)
        {
            var resultParam = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var UserIdParam = new SqlParameter("@UserId", id);

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteUser @UserId, @Result OUT",
                UserIdParam, resultParam
            );

            // Return the result value from the OUTPUT parameter
            return (int)resultParam.Value;
        }

        public async Task<List<UserDomainModel>> GetUsers()
        {
            // Fetch users using the stored procedure
            List<UserDomainModel> Users = await _context.users
                .FromSqlRaw("EXEC GetUsers")
                .ToListAsync(); // This will return a list of users

            // Eagerly load the Posts for each user using Collection (client-side)
            foreach (UserDomainModel User in Users)
            {
                // Use Collection() for loading collection navigation property (Posts)
                await _context.Entry(User)
                    .Collection(u => u.Posts) // Correctly use Collection() for loading Posts
                    .LoadAsync();
            }

            return Users;
        }

      
        public async Task<UserDomainModel> GetUsersByid(int id)
        {  // Create the SQL parameter for the stored procedure
           // Define the SQL query for the stored procedure
            var sql = "EXEC [dbo].[GetUserById] @UserId = {0}";

            // Execute the stored procedure and get the result as a list
            UserDomainModel User = _context.users
                .FromSqlRaw(sql, id)  // Pass the parameter to the SQL query
                .AsEnumerable()  // Move the query to the client side (client-side composition)
                .FirstOrDefault(); // Get the first student or null if not found

            // Optionally, load related entities (like Institute) if needed
            if (User != null)
            {
                // Explicitly load the related Institute navigation property
                await _context.Entry(User)
                    .Collection(u => u.Posts)
                    .LoadAsync();
            }

            return User;
        }
        public async Task<int> insert(UserDomainModel model)
        {
            // Output parameter to hold the UserId returned from the stored procedure
            var userIdParam = new SqlParameter("@UserId", SqlDbType.Int) { Direction = ParameterDirection.Output };

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC InsertUser @Username, @Email, @UserId OUT",
                new SqlParameter("@Username", model.Username),
                new SqlParameter("@Email", model.Email),
                userIdParam
            );

            // Return the UserId from the output parameter
            return (int)userIdParam.Value;
        }

        public async Task<bool> update(UserDomainModel model)
        {
            var resultParam = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateUser @UserId, @Username, @Email, @Result OUTPUT",
                new SqlParameter("@UserId", model.UserId),
                new SqlParameter("@Username", model.Username),
                new SqlParameter("@Email", model.Email),
                resultParam
            );

            return (int)resultParam.Value == 1;
        }
    }
}
