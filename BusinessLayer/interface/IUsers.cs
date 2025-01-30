using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IUsers
    {
        Task<List<UserDomainModel>> GetUsers();

        Task<UserDomainModel> GetUsersByid(int id);

        Task<int> insert(UserDomainModel model);

        Task<bool> update(UserDomainModel model);

        Task<bool> delete(int id);
    }
}
