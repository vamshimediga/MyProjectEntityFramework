using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Department
    {
        public int DepartmentID { get; set; } // Primary Key
        public string DepartmentName { get; set; }

        // Navigation Property
        public ICollection<Employee> Employees { get; set; }

        public static implicit operator Department(List<Department> v)
        {
            throw new NotImplementedException();
        }
    }
}
