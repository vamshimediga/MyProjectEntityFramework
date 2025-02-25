using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class ApartmentViewModel
    {
        public int ApartmentID { get; set; }
        public string ApartmentName { get; set; }
        public string Address { get; set; }
        public int TotalUnits { get; set; }

        public ICollection<ResidentViewModel> Residents { get; set; }
    }
}
