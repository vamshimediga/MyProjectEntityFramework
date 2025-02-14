using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class LawyerViewModel
    {
        public int LawyerID { get; set; }  // Primary Key
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; }
        public int Experience { get; set; }

        // Navigation Property (One Lawyer can have many Clients)
        public ICollection<ClientViewModel> Clients { get; set; } = new List<ClientViewModel>();
    }
}
