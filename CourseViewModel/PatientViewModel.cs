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
        [Key]
        public int PatientID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(15)]
        public string ContactNumber { get; set; }

        // Navigation property
        public ICollection<AppointmentViewModel> Appointments { get; set; }
    }
}
