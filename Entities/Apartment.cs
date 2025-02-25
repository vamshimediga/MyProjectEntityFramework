using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Apartment
    {
        public int ApartmentID { get; set; }
        public string ApartmentName { get; set; }
        public string Address { get; set; }
        public int TotalUnits { get; set; }

        // Navigation Property: One Apartment can have many Residents
        public ICollection<Resident> Residents { get; set; }
    }
}
