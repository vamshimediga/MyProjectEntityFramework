using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class AppointmentViewModel
    {

        public int AppointmentID { get; set; }


        public int PatientID { get; set; }


        public DateTime AppointmentDate { get; set; }


        public string DoctorName { get; set; }

        // Navigation property
        
        public PatientViewModel Patient { get; set; }
    }
}
