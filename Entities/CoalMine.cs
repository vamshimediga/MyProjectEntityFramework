using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CoalMine
    {
        [Key]
        public int MineID { get; set; }

       
        public string MineName { get; set; }

     
        public string Location { get; set; }

      
        public int CapacityInTons { get; set; }

      
        public DateTime EstablishedDate { get; set; }

        // Navigation property for related CoalProduction
       public virtual ICollection<CoalProduction> CoalProductions { get; set; }
    }
}
