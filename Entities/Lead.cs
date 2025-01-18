using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Lead
    {
        public int LeadID { get; set; }         // Primary Key
        public string FirstName { get; set; }   // First Name
        public string LastName { get; set; }    // Last Name
        public string Email { get; set; }       // Email

        // Navigation property for related Contacts
        public ICollection<Contact> Contacts { get; set; }
    }
}
