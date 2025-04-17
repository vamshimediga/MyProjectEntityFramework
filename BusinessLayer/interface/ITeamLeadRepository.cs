using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ITeamLeadRepository
    {
        Task<List<TeamLead>> GetAllTeamLeadsAsync();
        Task<TeamLead> GetTeamLeadByIdAsync(int id);
        Task<int> AddTeamLeadAsync(TeamLead teamLead);
        Task<bool> UpdateTeamLeadAsync(TeamLead teamLead);
        Task<bool> DeleteTeamLeadAsync(int id);
    }
}
