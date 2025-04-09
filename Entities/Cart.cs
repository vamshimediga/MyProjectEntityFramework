using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Cart
    {
        public int CartID { get; set; }
        public int AppointmentID { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;

        public Appointment Appointment { get; set; } // optional navigation
    }
}
