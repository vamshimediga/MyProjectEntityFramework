using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CompanyTypeDomainModel
    {
        [Key]
        public int CompanyTypeID { get; set; }
        public string CompanyTypeName { get; set; }
        public virtual ICollection<OpptypeDomainModel> Opptypes { get; set; }
    }
}
