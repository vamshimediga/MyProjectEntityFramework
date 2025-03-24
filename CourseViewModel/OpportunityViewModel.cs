using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class OpportunityViewModel
    {
        public int OpportunityID { get; set; }


        public string OpportunityName { get; set; }


        public decimal EstimatedValue { get; set; }

        public DateTime? CloseDate { get; set; }


        public string Stage { get; set; }

        // Navigation Property
        public List<ActivityViewModel> Activities { get; set; }
    }
}
