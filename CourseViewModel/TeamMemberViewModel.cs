using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class TeamMemberViewModel
    {
        public int TeamMemberId { get; set; }
        public string MemberName { get; set; }

        // Foreign Key
        public int TeamLeadId { get; set; }

        // Navigation property
        public TeamLeadViewModel TeamLead { get; set; }
    }
}
