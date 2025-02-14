using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Client
    {
        public int ClientID { get; set; }  // Primary Key
        public int LawyerID { get; set; }  // Foreign Key
        public string Name { get; set; } = string.Empty;
        public string CaseType { get; set; }

        // Navigation Property (Many-to-One)
        public Lawyer Lawyer { get; set; } 
    }
}
