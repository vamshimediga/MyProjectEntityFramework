using EntitiesViewModel;
using System;
using System.Collections.Generic;

namespace Entities
{
    public class CoalMineViewModel
    {
        public int MineID { get; set; }


        public string MineName { get; set; }


        public string Location { get; set; }


        public int CapacityInTons { get; set; }


        public DateTime EstablishedDate { get; set; }

        // Navigation property for related CoalProduction
       public virtual ICollection<CoalProductionViewModel> CoalProductions { get; set; }
    }
}
