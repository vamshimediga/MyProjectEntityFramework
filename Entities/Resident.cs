using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Resident
    {
        public int ResidentID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int ApartmentID { get; set; }  // Foreign Key

        // Navigation Property
        public Apartment Apartment { get; set; }
    }
}
