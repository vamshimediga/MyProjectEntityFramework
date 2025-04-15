using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class DeveloperViewModel
    {
        public int DeveloperId { get; set; }
        public string Name { get; set; }
        public string SkillSet { get; set; }

        public virtual List<SystemAdminViewModel> SystemAdmins { get; set; }
    }
}
