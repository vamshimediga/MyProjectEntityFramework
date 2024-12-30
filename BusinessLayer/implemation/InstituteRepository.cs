using Data;
using Entities;
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
        public Task<int> delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Institute> GetInstituteByid(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Institute>> GetInstitutes()
        {
            throw new NotImplementedException();
        }

        public Task<int> insert(Institute institute)
        {
            throw new NotImplementedException();
        }

        public Task<int> update(Institute institute)
        {
            throw new NotImplementedException();
        }
    }
}
