using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AgentDomainModel
    {
        public int AgentID { get; set; }   // Primary Key
        public string Name { get; set; }
        public ICollection<LeadAgentDomainModel> LeadAgents { get; set; }
    }
}
