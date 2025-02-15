using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IPassport
    {
        Task<List<Passport>> GetPassports();

        Task<Passport> GetPassportById(int id);

        Task<int> insert(Passport passport);
        Task<int> update(Passport passport);

        Task<int> delete(int id);   
    }
}
