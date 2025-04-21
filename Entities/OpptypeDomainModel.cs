using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OpptypeDomainModel
    {
        [Key]
        public int OpptypeID { get; set; }
        public string OpptypeName { get; set; }

        public int CompanyTypeID { get; set; }
        public virtual CompanyTypeDomainModel CompanyType { get; set; }
    }
}
