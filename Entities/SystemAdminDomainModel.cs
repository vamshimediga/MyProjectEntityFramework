using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SystemAdminDomainModel
    {
        [Key]
        public int SystemAdminId { get; set; }
        public string AdminName { get; set; }

        // Foreign key
        public int DeveloperId { get; set; }

        // Navigation property
        public virtual DeveloperDomainModel Developer { get; set; }
    }
}
