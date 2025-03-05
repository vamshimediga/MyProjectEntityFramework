using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class ApartmentViewModel
    {
        public int ApartmentID { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} cannot exceed 100 characters")]
        [Display(Name = "Apartment Name")]
        public string ApartmentName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} must be at least 1")]
        [Display(Name = "Total Units")]
        public int TotalUnits { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Resident ID")]
        public int ResidentId { get; set; }
        // Initialize the Residents list to prevent null reference errors
        public ICollection<ResidentViewModel> Residents { get; set; } = new List<ResidentViewModel>();
    }
}
