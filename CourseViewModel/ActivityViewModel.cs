using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class ActivityViewModel
    {
        public int ActivityID { get; set; }


        public string ActivityType { get; set; } // Example: Call, Email, Meeting


        public string Description { get; set; }

        public DateTime ActivityDate { get; set; } = DateTime.UtcNow;


        public int OpportunityID { get; set; }


         public OpportunityViewModel Opportunity { get; set; }
    }
}
