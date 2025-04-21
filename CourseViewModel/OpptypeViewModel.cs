using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class OpptypeViewModel
    {
        public int OpptypeID { get; set; }
        public string OpptypeName { get; set; }

        public int CompanyTypeID { get; set; }
        public virtual CompanyTypeViewModel CompanyType { get; set; }
    }
}
