using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EntitiesViewModel
{
    public class EmployeeViewModel
    {
        public int EmployeeID { get; set; } // Primary Key
        public string EmployeeName { get; set; }

        // Foreign Key
        public int DepartmentID { get; set; }

        // Navigation Property
        public DepartmentViewModel Department { get; set; }
    }
}
