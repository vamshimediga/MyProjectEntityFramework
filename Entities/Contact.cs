using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Contact
    {
        public int ContactID { get; set; }         // Primary Key
        public string ContactName { get; set; }    // Contact Name
        public string ContactPhone { get; set; }   // Contact Phone
        public int LeadID { get; set; }            // Foreign Key to Leads

        // Navigation property for related Lead
        public Lead Lead { get; set; }
    }
}
