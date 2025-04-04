using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Appointment
    {
       
        public int AppointmentID { get; set; }

     
        public int PatientID { get; set; }

       
        public DateTime AppointmentDate { get; set; }

       
        public string DoctorName { get; set; }

        // Navigation property
        [ForeignKey("PatientID")]
        public Patient Patient { get; set; }
    }
}
