using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class CompanyTypeViewModel
    {
        public int CompanyTypeID { get; set; }
        public string CompanyTypeName { get; set; }
        public virtual ICollection<OpptypeViewModel> Opptypes { get; set; }
    }
}
