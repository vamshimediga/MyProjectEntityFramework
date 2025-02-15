using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class DepartmentViewModel
    {
        //[Required(ErrorMessage = "Department ID is required")]
        //[Range(1, int.MaxValue, ErrorMessage = "Department ID must be greater than 0")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        [StringLength(100, ErrorMessage = "Department Name cannot exceed 100 characters")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}
