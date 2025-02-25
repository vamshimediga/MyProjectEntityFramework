using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CoalProduction
    {
        [Key]
        public int ProductionID { get; set; }

        
        public int ProductionYear { get; set; }

        
        public int TotalProductionInTons { get; set; }

        // Foreign Key
        [ForeignKey("CoalMine")]
        public int MineID { get; set; }

        // Navigation property
        public virtual CoalMine CoalMine { get; set; }
    }
}
