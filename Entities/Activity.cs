using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Entities
{
    public class Activity
    {
        
        public int ActivityID { get; set; }

    
        public string ActivityType { get; set; } // Example: Call, Email, Meeting

    
        public string Description { get; set; }

        public DateTime ActivityDate { get; set; } = DateTime.UtcNow;

       
        public int OpportunityID { get; set; }

    
        public Opportunity Opportunity { get; set; }
    }
}
