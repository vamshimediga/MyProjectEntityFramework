using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class ResidentViewModel
    {
        public int ResidentID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int ApartmentID { get; set; }  // Foreign Key

        // Navigation Property
        public ApartmentViewModel Apartment { get; set; }
    }
}
