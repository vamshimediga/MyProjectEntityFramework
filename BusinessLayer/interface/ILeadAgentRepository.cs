using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ILeadAgentRepository
    {
        Task<List<LeadAgentDomainModel>> GetAll();
        Task<LeadAgentDomainModel> GetByIdAsync(int id);
        Task<int> Add(LeadAgentDomainModel leadAgent);
        Task<bool> Update(LeadAgentDomainModel leadAgent);
        Task<bool> Delete(int id);
    }
}
