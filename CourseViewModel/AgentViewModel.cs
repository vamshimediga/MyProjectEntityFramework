using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class AgentViewModel
    {
        public int AgentID { get; set; }
        public string Name { get; set; }


        // Optional: for displaying associated lead agents
        public ICollection<LeadAgentViewModel> LeadAgents { get; set; } = new List<LeadAgentViewModel>();
    }
}
