using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class TeamLeadViewModel
    {
        public int TeamLeadId { get; set; }
        public string Name { get; set; }

        // Navigation property (1-to-many)
        public ICollection<TeamMemberViewModel> TeamMembers { get; set; }
    }
}
