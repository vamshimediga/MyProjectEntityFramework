using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class CoalProductionViewModel
    {
        [Key]
        public int ProductionID { get; set; }

        [Required]
        public int ProductionYear { get; set; }

        [Required]
        public int TotalProductionInTons { get; set; }

        // Foreign Key
        [ForeignKey("CoalMine")]
        public int MineID { get; set; }

        // Navigation property
        public virtual CoalMineViewModel CoalMine { get; set; }
    }
}
