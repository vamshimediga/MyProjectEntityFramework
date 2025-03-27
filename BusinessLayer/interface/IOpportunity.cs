using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IOpportunity
    {
        Task<List<Opportunity>> GetAllOpportunitiesAsync();
        Task<Opportunity> GetOpportunityByIdAsync(int opportunityId);
        Task<int> InsertOpportunityAsync(Opportunity opportunity);
        Task<bool> UpdateOpportunityAsync(Opportunity opportunity);
        Task<bool> DeleteOpportunityAsync(int opportunityId);
    }
}
