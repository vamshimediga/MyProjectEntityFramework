using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int InstituteId { get; set; }  // Foreign key to Institute
        public virtual Institute Institute { get; set; }  // Navigation property
    }
}
