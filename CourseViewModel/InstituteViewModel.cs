using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class InstituteViewModel
    {
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public virtual ICollection<StudentViewModel> Students { get; set; }  // One-to-many relationship
    }
}
