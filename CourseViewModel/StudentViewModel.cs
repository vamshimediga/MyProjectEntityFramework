using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int InstituteId { get; set; }  // Foreign key to Institute
        public virtual InstituteViewModel Institute { get; set; }  // Navigation property
    }
}
