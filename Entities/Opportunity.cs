using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Opportunity
    {
        
        public int OpportunityID { get; set; }

      
        public string OpportunityName { get; set; }

       
        public decimal EstimatedValue { get; set; }

        public DateTime? CloseDate { get; set; }

       
        public string Stage { get; set; }

        // Navigation Property
        public List<Activity> Activities { get; set; }
    }
}
