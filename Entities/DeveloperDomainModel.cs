using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DeveloperDomainModel
    {
        [Key]
        public int DeveloperId { get; set; }
        public string Name { get; set; }
        public string SkillSet { get; set; }

        public virtual List<SystemAdminDomainModel> SystemAdmins { get; set; }
    }
}
