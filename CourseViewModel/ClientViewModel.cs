using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class ClientViewModel
    {

        public int ClientID { get; set; }  // Primary Key
        public int LawyerID { get; set; }  // Foreign Key
        [Display(Name = "Client Name")]
        public string Name { get; set; } = string.Empty;
        public string CaseType { get; set; }

        // Navigation Property (Many-to-One)
        public LawyerViewModel Lawyer { get; set; }
        public List<LawyerViewModel> Lawyers { get; set; }= new List<LawyerViewModel>();
    }
}
