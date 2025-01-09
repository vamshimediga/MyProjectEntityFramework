using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; } // Primary Key
        public string EmployeeName { get; set; }

        // Foreign Key
        public int DepartmentID { get; set; }

        // Navigation Property
        public Department Department { get; set; }
    }
}
