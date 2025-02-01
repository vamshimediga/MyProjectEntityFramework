using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
        // Navigation property for related passports (one-to-many)
        public ICollection<Passport> Passports { get; set; }
    }

}
