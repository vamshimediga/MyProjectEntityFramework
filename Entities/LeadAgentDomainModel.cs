using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LeadAgentDomainModel
    {
        public int LeadAgentID { get; set; }   // Primary Key
        public string LeadName { get; set; }

        public int AgentID { get; set; }       // Foreign Key
        public AgentDomainModel Agent { get; set; }
    }
}
