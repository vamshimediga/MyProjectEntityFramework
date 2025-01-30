using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public Task<bool> delete(int id)
        {
            throw new NotImplementedException();
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

        public Task<UserDomainModel> GetUsersByid(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> insert(UserDomainModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> update(UserDomainModel model)
        {
            throw new NotImplementedException();
        }
    }
}
