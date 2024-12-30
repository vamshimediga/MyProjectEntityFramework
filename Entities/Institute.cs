using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Institute
    {
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
       public ICollection<Student> Students { get; set; }  // One-to-many relationship
    }
}
