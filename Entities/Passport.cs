using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Passport
    {
        public int PassportId { get; set; }
        public string PassportNumber { get; set; }

        // Foreign key for the related person
        public int PersonId { get; set; }

   
        // Navigation property for the related person
        public Person Person { get; set; }
    }
}
