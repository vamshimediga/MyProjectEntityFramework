using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ISystemAdmin
    {
        Task<List<SystemAdminDomainModel>> GetAllAsync();
        Task<SystemAdminDomainModel> GetByIdAsync(int systemAdminId);
        Task<int> InsertAsync(SystemAdminDomainModel admin);
        Task<bool> UpdateAsync(SystemAdminDomainModel admin);
        Task<bool> DeleteAsync(int systemAdminId);
    }
}
