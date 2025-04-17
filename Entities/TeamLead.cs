using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TeamLead
    {
        public int TeamLeadId { get; set; }
        public string Name { get; set; }

        // Navigation property (1-to-many)
        public ICollection<TeamMember> TeamMembers { get; set; }
    }
}
