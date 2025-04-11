using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IAgentRepository
    {
        Task<List<AgentDomainModel>> GetAllAsync();
        Task<AgentDomainModel> GetByIdAsync(int id);
        Task<int> InsertAsync(AgentDomainModel agent);
        Task<bool> UpdateAsync(AgentDomainModel agent);
        Task<bool> DeleteAsync(int id);
    }
}
