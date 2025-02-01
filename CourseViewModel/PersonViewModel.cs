using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }
        public string Name { get; set; }

        public int? Active { get; set; }
        // Navigation property for related passports (one-to-many)
        public ICollection<PassportViewModel> Passports { get; set; }
    }
}
