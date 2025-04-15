using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class SystemAdminViewModel
    {
        public int SystemAdminId { get; set; }
        public string AdminName { get; set; }

        // Foreign key
        public int DeveloperId { get; set; }

        // Navigation property
        public virtual DeveloperViewModel Developer { get; set; }
    }
}
