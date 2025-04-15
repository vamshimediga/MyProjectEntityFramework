using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly ApplicationDbContext _context;

        public DeveloperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Add a developer
        public async Task<int> Add(DeveloperDomainModel developer)
        {
            await _context.Developers.AddAsync(developer);
            return await _context.SaveChangesAsync();
        }

        // 2. Get all developers with their system admins
        public async Task<List<DeveloperDomainModel>> GetAll()
        {
            return await _context.Developers
                                 .Include(d => d.SystemAdmins)
                                 .ToListAsync();
        }

        // 3. Get developer by ID
        public async Task<DeveloperDomainModel> GetById(int id)
        {
            return await _context.Developers
                                 .Include(d => d.SystemAdmins)
                                 .FirstOrDefaultAsync(d => d.DeveloperId == id);
        }

        // 4. Update developer
        public async Task<bool> Update(DeveloperDomainModel developer)
        {
            _context.Developers.Update(developer);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        // 5. Delete developer by ID
        public async Task<bool> Delete(int id)
        {
            var developer = await _context.Developers.FindAsync(id);
            if (developer != null)
            {
                _context.Developers.Remove(developer);
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }

            return false;
        }
    }
}
