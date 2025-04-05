using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Patient
    {
    
        public int PatientID { get; set; }

      
        public string FirstName { get; set; }

       
        public string LastName { get; set; }

        
        public string ContactNumber { get; set; }

        // Navigation property
        public ICollection<Appointment> Appointments { get; set; }
    }
}
