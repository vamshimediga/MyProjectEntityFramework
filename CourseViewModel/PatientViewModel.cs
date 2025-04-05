using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class PatientViewModel
    {
        
        public int PatientID { get; set; }

        public string FirstName { get; set; }

     
        public string LastName { get; set; }

      
        public string ContactNumber { get; set; }

        // Navigation property
        public ICollection<AppointmentViewModel> Appointments { get; set; }
    }
}
