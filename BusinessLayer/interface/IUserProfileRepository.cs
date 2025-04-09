using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IUserProfileRepository
    {
        Task<List<UserProfile>> GetAllAsync();
        Task<UserProfile> GetByIdAsync(int id);
        Task<int> AddAsync(UserProfile userProfile);
        Task<bool> UpdateAsync(UserProfile userProfile);
        Task<bool> DeleteAsync(int id);
    }
}
