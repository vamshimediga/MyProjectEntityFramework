using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TeamMember
    {
        public int TeamMemberId { get; set; }
        public string MemberName { get; set; }

        // Foreign Key
        public int TeamLeadId { get; set; }

        // Navigation property
        public TeamLead TeamLead { get; set; }
    }
}
